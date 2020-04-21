using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.ViewModel
{
    public class UserManagement
    {
        public class UserManagementGrid
        {
            public string ID { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string CreatedBy { get; set; }

            public string Status { get; set; }

            public string Mobile { get; set; }
        }

        public class SearchUserManagement
        {
            public UserManagementCriteria User { get; set; }
        }


        public class UserManagementCriteria
        {
            public string Name { get; set; }

            public string Status { get; set; }

            public string Users { get; set; }
        }


        public class UserManagementData
        {
            public List<string> Permissions { get; set; }

            public string Status { get; set; }

            public UserManagementProperties userManagement { get; set; }
        }

        public class UserManagementProperties
        {

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Password { get; set; }
        }
    }
}
