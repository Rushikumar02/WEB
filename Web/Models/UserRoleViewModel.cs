using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserRoleViewModel
    {
        public string EmployeeId { get; set; }
        public string RoleId { get; set; } 
        public string EmailId { get; set; }
        public Boolean IsSelected { get; set; }
    }
}