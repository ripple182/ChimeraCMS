using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Uploads
{
    public class Icon
    {
        public string ClassValue { get; set; }

        public string DisplayName { get; set; }

        public Icon()
        {
            ClassValue = string.Empty;
            DisplayName = string.Empty;
        }
    }
}
