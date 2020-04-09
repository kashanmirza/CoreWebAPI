using CoreWebAPI.Helpers;
using CoreWebAPI.Models;
using CoreWebAPI.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebAPI.Services
{
  
    public interface IUserService
    {
        vmUser Authenticate(string username, string password);
        IEnumerable<vmUser> GetAll();

        List<SecPermissions> GetAllPermissions();
    }

    public class UserService : IUserService
    {
        CoreDBContext db = new CoreDBContext();

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<vmUser> _users = new List<vmUser>();
        //{
        //    new vmUser { Id = 1, FirstName = "Test", LastName = "User", UserName = "test", Password = "test" ,IntialName ="TT" ,permissions = permissionsList() , RoleName="Admin"  }
        //    ,new vmUser { Id = 2, FirstName = "Yasir", LastName = "User", UserName = "yasir", Password = "yasir",IntialName ="YR",permissions = permissionsList() , RoleName="Admin"}
        //    ,new vmUser { Id = 3, FirstName = "Nasir", LastName = "User", UserName = "nasir", Password = "nasir",IntialName ="NR",permissions = permissionsListMemployee(), RoleName="Employee" }
        //};

        public static List<SecPermissions> permissionsList()
        {
            return new List<SecPermissions>();
        }

        public static List<SecPermissions> permissionsListMemployee()
        {
            return new List<SecPermissions>();
        }

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public vmUser Authenticate(string username, string password)
        {
            vmUser _user = new vmUser();
            var user = db.SecUsers.SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // JWT Implementation

            var ClaimData = new[] { new Claim(ClaimTypes.Name ,username), new Claim(ClaimTypes.Role, "admin") };
            var _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var SignInCred = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);

            var _token = new JwtSecurityToken(
                issuer: "localhost:5001",
                audience: "localhost:5001",
                expires: DateTime.Now.AddMinutes(5),
                claims:ClaimData,
                signingCredentials: SignInCred
                );

            var Token = new JwtSecurityTokenHandler().WriteToken(_token);

            //return OK(Token);

            // JWT END


            // authentication successful so generate jwt token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddMinutes(5),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = Token; // tokenHandler.WriteToken(token);
            user.TokenExpireOn = DateTime.UtcNow.AddMinutes(1);
            user.Password = null;
            MappingProperty.Map(_user, user);
            var userRoles = db.SecUserRoles.Where(x => x.SecUserId == user.SecUserId).Select(x=>x.SecRoleId).ToList();
            var userRolePermissions=  db.SecRolePermissions.Where(x => userRoles.Contains(x.SecRoleId)).Select(x=>x.SecPermissionId).ToList();
            var userPermissions = db.SecPermissions.Where(x => userRolePermissions.Contains(x.SecPermissionId)).ToList();

            _user.permissions = userPermissions;
            _user.RoleName = db.SecRoles.Where(x=>x.SecRoleId == userRoles[0]).FirstOrDefault().RoleName;

            return _user;
        }

        public IEnumerable<vmUser> GetAll()
        {
            // return users without passwords
            return _users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

        public List<SecPermissions> GetAllPermissions()
        {
            var a = permissionsList();
            return a.ToList();
        }



    }
}

