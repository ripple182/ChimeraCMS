using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Page.Module
{
    public class ColumnModule : ModuleBase
    {
        /*
         * when i load the partial view, I load a dictionary of "module rules" as well so that I'm able to add special classes for drag & drop editing.
         * the editable children types and value dictionary keys will be stored in the partial views.
         */

        /// <summary>
        /// The development name of the module used.
        /// </summary>
        public string ModuleTypeName { get; set; }

        /// <summary>
        /// Dictionary to hold the values of particular children of this column module.
        /// For example a "TitleDescription" module has a title and description.
        /// So we would have two values in this dictionary created from the partial view "Title", "Description".
        /// if dictionary does not contain the key then that means it was remove during the editing process.
        /// </summary>
        public Dictionary<string, ColumnModuleChild> ChildrenValueDictionary { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ColumnModule()
        {
            AdditionalClassNames = string.Empty;
            ModuleTypeName = string.Empty;
            ChildrenValueDictionary = new Dictionary<string, ColumnModuleChild>();
        }
    }
}
