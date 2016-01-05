using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Dashboard
{
    public class MvcUrl
    {
        public string Action { get; set; }

        public string Controller { get; set; }

        public string IdParameterName { get; set; }

        public MvcUrl()
        {
            Action = string.Empty;
            Controller = string.Empty;
            IdParameterName = "id";
        }

        public MvcUrl(string action, string controller)
        {
            Action = action;
            Controller = controller;
            IdParameterName = "id";
        }

        public string GenerateFullUrl(string entityId)
        {
            return String.Format("~/Admin/{0}/{1}?{2}={3}",Controller, Action, IdParameterName, entityId);
        }
    }
}
