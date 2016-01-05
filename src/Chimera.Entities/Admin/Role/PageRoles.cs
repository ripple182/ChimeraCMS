using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class PageRoles
    {
        public const string VIEW = "page-view";

        public const string EDIT = "page-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT;
            }
        }
    }
}
