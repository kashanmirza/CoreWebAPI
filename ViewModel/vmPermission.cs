using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.ViewModel
{
    public class vmPermission
    {
     
            public int? Id { get; set; }
            public int? ParentPermissionId { get; set; }
            public string Name { get; set; }
            public string PermissionName { get; set; }
            public string ParentPermissionName { get; set; }
            public string ApiURL { get; set; }
            public string ComponentName { get; set; }
            public string ComponentURL { get; set; }
            public string icon { get; set; }
            public string Text { get; set; }
            public string Type { get; set; }
            public int? IsModule { get; set; }
            public int? IsDefaultOpen { get; set; }
            public int? Sequence { get; set; }
            public DateTime? CreatedOn { get; set; }
        
    }
}
