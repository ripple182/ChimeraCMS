using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Page.Module
{
    public class RowModule : ModuleBase
    {
        /// <summary>
        /// List of column modules that make up this row.
        /// </summary>
        public List<ColumnModule> ColumnModuleList { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public RowModule()
        {
            AdditionalClassNames = string.Empty;
            ColumnModuleList = new List<ColumnModule>();
        }
    }
}
