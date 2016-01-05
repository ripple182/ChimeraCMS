using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChimeraWebsite.Models.Editor
{
    public class KnockoutEditableFieldModel
    {
        
        /// <summary>
        /// The actual column module to display.
        /// </summary>
        public ColumnModuleModel ColumnModuleModel { get; set; }

        /// <summary>
        /// The actual html element that will be holding the value, p, span, etc.
        /// </summary>
        public string HtmlElement { get; set; }

        /// <summary>
        /// What is the actual dictionary key for the value we desire.
        /// </summary>
        public string KeyForValue { get; set; }

        /// <summary>
        /// How should the field be edited?  text / html / image / etc.
        /// </summary>
        public string EditType { get; set; }

        public string NonClassAttributes { get; set; }

        public string ClassAttribute { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public KnockoutEditableFieldModel()
        {
            ColumnModuleModel = new ColumnModuleModel();
        }

        /// <summary>
        /// Constructor.
        /// </summary> 
        /// <param name="columnModuleModel"></param>
        /// <param name="htmlElement"></param>
        /// <param name="keyForValue"></param>
        /// <param name="editType"></param>
        public KnockoutEditableFieldModel(ColumnModuleModel columnModuleModel, string htmlElement, string keyForValue, string editType, string nonClassAttributes, string classAttribute)
        {
            ColumnModuleModel = columnModuleModel;
            HtmlElement = htmlElement;
            KeyForValue = keyForValue;
            EditType = editType;
            NonClassAttributes = nonClassAttributes;
            ClassAttribute = classAttribute;
        }
    }
}