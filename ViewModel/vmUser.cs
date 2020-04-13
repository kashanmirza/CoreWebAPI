using CoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.ViewModel
{
    public class vmUser
    { 
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int? OtpEmail { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public int? OtpPhoneNumber { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpireOn { get; set; }
        public string Channel { get; set; }
        public bool? Locked { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IntialName { get; set; }
        public string RoleName { get; set; }
        public List<SecPermissions> permissions { get; set; }

        public static implicit operator vmUser(List<vmUser> v)
        {
            throw new NotImplementedException();
        }
    }

}
