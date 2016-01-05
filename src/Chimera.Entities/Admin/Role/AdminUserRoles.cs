using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class AdminUserRoles
    {
        public const string VIEW = "admin-user-view";

        public const string EDIT = "admin-user-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT;
            }
        }
    }
}
