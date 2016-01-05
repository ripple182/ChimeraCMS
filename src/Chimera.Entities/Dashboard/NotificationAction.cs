using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Dashboard
{
    public class NotificationAction
    {
        /// <summary>
        /// Name of the action, "Mark As Approved", "Mark as Read", etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If an icon should be used to identify this action.
        /// </summary>
        public string IconClass { get; set; }


        /// <summary>
        /// The mvc url object used to figure out where to redirect the admin user when they click
        /// </summary>
        public MvcUrl MvcUrl { get; set; }

        /// <summary>
        /// The list of admin user roles required to see and execute this action.
        /// </summary>
        public List<string> ViewAdminUserRolesRequired { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public NotificationAction()
        {
            Name = string.Empty;
            IconClass = string.Empty;
            MvcUrl = new MvcUrl();
            ViewAdminUserRolesRequired = new List<string>();
        }

        /// <summary>
        /// Constructor used to create the automatic "dismiss" action for notifications.
        /// </summary>
        /// <param name="name">Dism</param>
        /// <param name="iconClass"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        public NotificationAction(string name, string iconClass, string action, string controller)
        {
            Name = name;
            IconClass = iconClass;
            MvcUrl = new MvcUrl(action, controller);
            ViewAdminUserRolesRequired = new List<string>();
        }
    }
}
