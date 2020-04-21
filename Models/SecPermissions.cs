using System;
using System.Collections.Generic;

namespace CoreWebAPI.Models
{
    public partial class Permission
    {
        public int SecPermissionId { get; set; }
        public string PermissionName { get; set; }
        public int? ParentPermissionId { get; set; }
        public string ParentPermissionName { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public int? IsModule { get; set; }
        public string ComponentName { get; set; }
        public string ComponentUrl { get; set; }
        public string ApiUrl { get; set; }
        public string Type { get; set; }
        public int? IsDefaultOpen { get; set; }
        public int? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
    }

   
}
