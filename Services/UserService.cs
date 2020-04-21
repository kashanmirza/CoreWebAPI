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

        List<vmPermission> GetAllPermissions();

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

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<vmUser> _users = new List<vmUser>();

        public static List<vmPermission> permissionsList()
        {
            return new List<vmPermission> {
                new vmPermission { Id = 1,ParentPermissionId = null, PermissionName = "Welcome" ,ParentPermissionName=null, IsModule=1
                ,Text="Home" ,ComponentName="Home",ComponentURL="/" , ApiURL="/home/getModules" ,Type = "Module" ,IsDefaultOpen=1 },

                new vmPermission { Id = 2,ParentPermissionId = null, PermissionName = "MembershipManagment",ParentPermissionName=null , IsModule=1
                ,Text="Membership Managment" ,ComponentName="MembershipManagement",ComponentURL="/Membership/MembershipManagement" , ApiURL="/Membership/getAllMembership" ,Type = "Module" ,IsDefaultOpen=0 },

                new vmPermission { Id = 3,ParentPermissionId = null, PermissionName = "MembershipRequest",ParentPermissionName=null, IsModule=1
                ,Text="Membership Request" ,ComponentName="MembershipRequest",ComponentURL="/Membership/MembershipRequest" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Module" ,IsDefaultOpen=0  },

                new vmPermission { Id = 4,ParentPermissionId = null, PermissionName = "EventRequest" ,ParentPermissionName=null, IsModule=1
                ,Text="Event Request" ,ComponentName="EventRequest",ComponentURL="/Event/EventRequest" , ApiURL="/Event/getAllEventRequest" ,Type = "Module" ,IsDefaultOpen=0 },

                  new vmPermission { Id = 5,ParentPermissionId = 3, PermissionName = "MembershipRequest-ViewDetail",ParentPermissionName="EventRequest" , IsModule=0
                ,Text="Request Detail" ,ComponentName="MembershipRequestDetail",ComponentURL="/Membership/MembershipRequestDetail" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Page" ,IsDefaultOpen=0  },

                    new vmPermission { Id = 6,ParentPermissionId = 5, PermissionName = "MembershipRequest-Approve",ParentPermissionName="MembershipRequest-ViewDetail", IsModule=0
                ,Text="Aprove" ,ComponentName="",ComponentURL="" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Action" ,IsDefaultOpen=0  },

                 new vmPermission { Id = 7,ParentPermissionId = 5, PermissionName = "MembershipRequest-Reject",ParentPermissionName="MembershipRequest-ViewDetail", IsModule=0
                ,Text="Reject" ,ComponentName="",ComponentURL="" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Action" ,IsDefaultOpen=0  },

                   new vmPermission { Id = 8,ParentPermissionId = 5, PermissionName = "MembershipRequest-Create",ParentPermissionName="MembershipRequest-ViewDetail", IsModule=0
                ,Text="Create" ,ComponentName="",ComponentURL="" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Action" ,IsDefaultOpen=0  },

new vmPermission { Id = 9,ParentPermissionId = 11, PermissionName = "RoleManagement-Add" ,ParentPermissionName="RoleManagement", IsModule=0
,Text="Add Role" ,ComponentName="Role",ComponentURL="/Role/Role" , ApiURL="/Role/GetAllPermisssions" ,Type = "Page" ,IsDefaultOpen=1 },

new vmPermission { Id = 10,ParentPermissionId = 11, PermissionName = "RoleManagement-Edit" ,ParentPermissionName="RoleManagement", IsModule=0
,Text="Edit Role" ,ComponentName="EditRole",ComponentURL="/Role/EditRole" , ApiURL="/Role/GetAllPermisssions" ,Type = "Page" ,IsDefaultOpen=1 },

new vmPermission { Id = 11,ParentPermissionId = null, PermissionName = "RoleManagement" ,ParentPermissionName=null, IsModule=1
,Text="Role Management" ,ComponentName="SearchRole",ComponentURL="/Role/SearchRole" , ApiURL="/Role/SaveRolePermisssions" ,Type = "Module" ,IsDefaultOpen=1 },

new vmPermission { Id = 12,ParentPermissionId = null, PermissionName = "UserManagement" ,ParentPermissionName=null, IsModule=1
,Text="User Management" ,ComponentName="SearchUser",ComponentURL="/UserManagement/SearchUser" , ApiURL="/Role/SearchAll" ,Type = "Module" ,IsDefaultOpen=1 },

new vmPermission { Id = 13,ParentPermissionId = 12, PermissionName = "UserManagement-Add" ,ParentPermissionName="UserManagement", IsModule=0
,Text="Add User" ,ComponentName="AddUser",ComponentURL="/UserManagement/AddUser" , ApiURL="/Role/GetActiveRole" ,Type = "Page" ,IsDefaultOpen=1 },

new vmPermission { Id = 14,ParentPermissionId = 12, PermissionName = "UserManagement-Edit" ,ParentPermissionName="UserManagement", IsModule=0
,Text="Edit User" ,ComponentName="EditUser",ComponentURL="/UserManagement/EditUser" , ApiURL="/Role/GetUserManagementPermisssionsById" ,Type = "Page" ,IsDefaultOpen=1 },

            };
        }

        public static List<vmPermission> permissionsListMemployee()
        {
            return new List<vmPermission> {
                new vmPermission { Id = 1, PermissionName = "Welcome-Landing" ,ParentPermissionName=null, IsModule=1
                ,Text="Home" ,ComponentName="Home",ComponentURL="/home" , ApiURL="/home/getModules" ,Type = "Module" ,IsDefaultOpen=1 },

                new vmPermission { Id = 2, PermissionName = "Membership-Managment",ParentPermissionName=null , IsModule=1
                ,Text="Membership Managment" ,ComponentName="MembershipManagement",ComponentURL="/Membership/MembershipManagement" , ApiURL="/Membership/getAllMembership" ,Type = "Module" ,IsDefaultOpen=0 },

                  new vmPermission { Id = 3,ParentPermissionId = null, PermissionName = "MembershipRequest",ParentPermissionName=null, IsModule=1
                ,Text="Membership Request" ,ComponentName="MembershipRequest",ComponentURL="/Membership/MembershipRequest" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Module" ,IsDefaultOpen=0  },


                  new vmPermission { Id = 5,ParentPermissionId = 3, PermissionName = "MembershipRequest-ViewDetail",ParentPermissionName="EventRequest" , IsModule=0
                ,Text="Request Detail" ,ComponentName="MembershipRequestDetail",ComponentURL="/Membership/MembershipRequestDetail" , ApiURL="/Membership/getAllMembershipRequest" ,Type = "Page" ,IsDefaultOpen=0  },


                new vmPermission { Id = 9,ParentPermissionId = null, PermissionName = "RoleManagement" ,ParentPermissionName=null, IsModule=1
                ,Text="Role Management" ,ComponentName="Role",ComponentURL="/Role/Role" , ApiURL="/home/getModules" ,Type = "Module" ,IsDefaultOpen=1 },


                 };
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
            user.TokenExpireOn = DateTime.Now.AddMinutes(5);
            db.SecUsers.Update(user);
            db.SaveChanges();
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

        public List<vmPermission> GetAllPermissions()
        {
            var a = permissionsList();
            return a.ToList();
        }


        #region Added by kashan


        public async Task<List<SecUsers>> GetUsers(vmUser criteria)
        {
            if (db != null)
            {
                try
                {
                    if (criteria.Status != null)
                    {
                        criteria.Status = int.Parse(criteria.Status.ToString());
                    }
                    Expression<Func<SecUsers, bool>> userName = res => res.UserName == criteria.UserName;
                    Expression<Func<SecUsers, bool>> status = res => res.Status == criteria.Status;
                    Expression<Func<SecUsers, bool>> createdBy = res => res.CreatedBy == criteria.CreatedBy;
                    IQueryable<SecUsers> filter = db.SecUsers;


                    if (!String.IsNullOrEmpty(criteria.UserName) && criteria.UserName != null)
                    {
                        filter = filter.Where(userName);
                    }
                    if (criteria.Status != null && criteria.Status != -1)
                    {
                        filter = filter.Where(status);
                    }
                    if (criteria.CreatedBy != null && criteria.CreatedBy != -1)
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
                    //vmUser u =  await (from p in db.SecUsers where p.CreatedBy == users.CreatedBy select new vmUser { Id = p.SecUserId}).FirstOrDefaultAsync();
                    //users.CreatedBy = u.Id;
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

