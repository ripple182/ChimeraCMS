using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class NavMenuRoles
    {
        public const string EDIT = "nav-menu-edit";

        public const string NEW = "nav-menu-new";

        public static string ALL
        {
            get
            {
                return NEW + ";" + EDIT;
            }
        }
    }
}
