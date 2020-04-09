﻿using System;
using System.Collections.Generic;

namespace CoreWebAPI.Models
{
    public partial class SecRolesInterim
    {
        public int SecRoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
        public int? Status { get; set; }
        public string Comments { get; set; }
    }
}
