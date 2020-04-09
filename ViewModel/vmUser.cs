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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string IntialName { get; set; }

        public string RoleName { get; set; }
        public List<SecPermissions> permissions { get; set; }
        public DateTime? TokenCreatedOn { get; set; }
        public DateTime? TokenExpireOn { get; set; }
    }

}
