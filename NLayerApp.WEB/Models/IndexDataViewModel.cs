using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayerApp.WEB.Models
{
    public class IndexDataViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}