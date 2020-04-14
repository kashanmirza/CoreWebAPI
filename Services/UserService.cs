using CoreWebAPI.Helpers;
using CoreWebAPI.Models;
using CoreWebAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
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

        #region Added By Kashan
        Task<List<SecUsers>> GetUsers(vmUser criteria);

        Task<vmUser> GetUser(int? Id);

        Task<int> CreateUser(SecUsers user);

        Task<int> DeleteUser(int? userId);

        Task UpdateUser(SecUsers user);

        #endregion
    }

    public class UserService : IUserService
    {
        CoreDBContext db = new CoreDBContext();
        IQueryable<SecUsers> empDetailsVar;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<vmUser> _users = new List<vmUser>();

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


        #region Added by kashan


        public async Task<List<SecUsers>> GetUsers(vmUser criteria)
        {
            List<SecUsers> users = new List<SecUsers>();


            if (db != null)
            {
                try
                {
                    Expression<Func<SecUsers, bool>> userName = res => res.UserName == criteria.UserName;
                    Expression<Func<SecUsers, bool>> status = res => res.Status == criteria.Status;
                    Expression<Func<SecUsers, bool>> createdBy = res => res.CreatedBy == criteria.CreatedBy;
                    IQueryable<SecUsers> filter = db.SecUsers;


                    if (!String.IsNullOrEmpty(criteria.UserName) || criteria.UserName != null)
                    {
                        filter = filter.Where(userName);
                    }
                    if (criteria.Status != null || criteria.Status > -1)
                    {
                        filter = filter.Where(status);
                    }
                    if (criteria.CreatedBy != null || criteria.Status > -1)
                    {
                        filter = filter.Where(createdBy);
                    }

                    return await filter.ToListAsync();


                }
                catch (Exception ex) {

                    Console.WriteLine("Some Error Acquired" + ex.StackTrace);
                }
               
            }

            return null;
        }

        public async Task<vmUser> GetUser(int? Id)
        {
            if (db != null)
            {
                return await (from p in db.SecUsers
                              where p.SecUserId == Id
                              select new vmUser
                              {
                                  Id = p.SecUserId,
                                  UserName = p.UserName,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,
                                  Email = p.Email,
                                  Password = p.Password,
                                  PhoneNumber = p.PhoneNumber

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> CreateUser(SecUsers users)
        {

            try
            {
                if (db != null)
                {
                    await db.SecUsers.AddAsync(users);
                    int id = await db.SaveChangesAsync();
                    return id;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
             
            }
            return 0;
        }

        public async Task<int> DeleteUser(int? userId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var user = await db.SecUsers.FirstOrDefaultAsync(x => x.SecUserId == userId);

                if (user != null)
                {
                    //Delete that post
                    db.SecUsers.Remove(user);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task UpdateUser(SecUsers user)
        {
            if (db != null)
            {
                try
                {
                    SecUsers record = await db.SecUsers.FirstOrDefaultAsync(x => x.SecUserId == user.SecUserId);
                    if (record != null)
                    {
                        record.UserName = user.UserName;
                        record.Password = user.Password;
                        record.FirstName = user.FirstName;
                        record.LastName = user.LastName;
                        record.Email = user.Email;
                        record.PhoneNumber = user.PhoneNumber;
                        record.UpdatedOn = user.UpdatedOn;
                    }

                    //update that post
                    db.SecUsers.Update(record);

                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: ", ex.StackTrace);
                }
            }
        }

    }
    #endregion
}

