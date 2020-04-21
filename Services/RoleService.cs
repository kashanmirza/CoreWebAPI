using CoreWebAPI.Models;
using CoreWebAPI.Helpers;
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
    public interface IRoleService
    {

        Task<List<SecRoles>> GetRoles(vmRoles criteria);

        Task<vmRoles> GetRole(int? Id);

        Task<int> CreateRole(SecRoles role);

        Task<int> DeleteRole(int? roleId);

        Task UpdateRole(SecRoles role);

        List<vmPermission> GetAllPermissions();

        List<Dropdown> GetAllUsers();


    }

    public class RoleService : IRoleService
    {
        private IUserService _userService;

        private readonly AppSettings _appSettings;

        public RoleService(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }


        CoreDBContext db = new CoreDBContext();

        public async Task<List<SecRoles>> GetRoles(vmRoles criteria)
        {

            if (db != null)
            {
                try
                {
                    Expression<Func<SecRoles, bool>> roleName = res => res.RoleName == criteria.RoleName;
                    Expression<Func<SecRoles, bool>> isActive = res => res.IsActive == criteria.IsActive;
                    Expression<Func<SecRoles, bool>> createdBy = res => res.CreatedBy == criteria.CreatedBy;
                    IQueryable<SecRoles> filter = db.SecRoles;


                    if (!String.IsNullOrEmpty(criteria.RoleName) && criteria.RoleName != null)
                    {
                        filter = filter.Where(roleName);
                    }
                    if (criteria.IsActive != null)
                    {
                        filter = filter.Where(isActive);
                    }
                    if (criteria.CreatedBy != null && criteria.CreatedBy != -1)
                    {
                        filter = filter.Where(createdBy);
                    }

                    return await filter.ToListAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Some Error Acquired" + ex.StackTrace);
                }

            }

            return null;
        }

        public async Task<vmRoles> GetRole(int? Id)
        {
            if (db != null)
            {
                return await (from p in db.SecRoles
                              where p.SecRoleId == Id
                              select new vmRoles
                              {
                                  Id = p.SecRoleId,
                                  RoleName = p.RoleName,
                                  IsActive = p.IsActive,
                                  CreatedBy = p.CreatedBy,
                                  Description = p.Description

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> CreateRole(SecRoles role)
        {

            try
            {
                if (db != null)
                {
                    await db.SecRoles.AddAsync(role);
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

        public async Task<int> DeleteRole(int? roleId)
        {
            int result = 0;

            if (db != null)
            {
                var role = await db.SecRoles.FirstOrDefaultAsync(x => x.SecRoleId == roleId);

                if (role != null)
                {
                    db.SecRoles.Remove(role);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task UpdateRole(SecRoles role)
        {
            if (db != null)
            {
                try
                {
                    SecRoles record = await db.SecRoles.FirstOrDefaultAsync(x => x.SecRoleId == role.SecRoleId);
                    if (record != null)
                    {
                        record.RoleName = role.RoleName;
                        record.IsActive = role.IsActive;
                        record.Description = role.Description;
                        record.UpdatedBy = 1;
                        record.UpdatedOn = role.UpdatedOn;
                        record.CreatedBy = role.CreatedBy;
                        record.CreatedOn = role.CreatedOn;
                    }

                    db.SecRoles.Update(record);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: ", ex.StackTrace);
                }
            }
        }


        public List<vmPermission> GetAllPermissions()
        {
            var a = _userService.GetAllPermissions();
            return a.ToList();
        }

        public List<Dropdown> GetAllUsers()
        {
            var a = _userService.GetAll().Select(c => new Dropdown { value = c.Id.ToString(), lable = c.FirstName }).ToList(); ;
            return a.ToList();
        }

    }
}