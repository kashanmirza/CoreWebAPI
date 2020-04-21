using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.ViewModel
{
    public class RoleNode
    {
        public int? value { get; set; }

        public string label { get; set; }

        public List<RoleNode> children { get; set; }
    }

    public class RoleGrid
    {
        public string ID { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string Status { get; set; }
    }


    public class RoleData
    {
        public List<string> Permissions { get; set; }

        public string Status { get; set; }

        public RoleProperties Role { get; set; }
    }

    public class RoleProperties
    {
        public string Comments { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class Dropdown
    {
        public string value { get; set; }

        public string lable { get; set; }
    }

    public class IdData
    {
        public PermissionIdPropperty Permission { get; set; }
    }


    public class PermissionIdPropperty
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }


    public class SearchRole
    {
        public RoleCriteria Role { get; set; }
    }


    public class RoleCriteria
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public string Users { get; set; }
    }
}
