using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ChimeraWebsite.Models
{
    public class ColumnModuleModel
    {
        /// <summary>
        /// Whether we are displaying this in ediable mode or not.
        /// </summary>
        public bool InEditMode { get; set; }

        /// <summary>
        /// The actual column module to display.   
        /// </summary>
        public Chimera.Entities.Page.Module.ColumnModule ColumnModule { get; set; }

        public ControllerContext ControllerContext { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColumnModuleModel()
        {
            InEditMode = false;
            ColumnModule = new Chimera.Entities.Page.Module.ColumnModule();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inEditMode"></param>
        /// <param name="columnModule"></param>
        public ColumnModuleModel(ControllerContext controllerContext, bool inEditMode, Chimera.Entities.Page.Module.ColumnModule columnModule)
        {
            ControllerContext = controllerContext;
            InEditMode = inEditMode;
            ColumnModule = columnModule;
        }

        /// <summary>
        /// Grab the child value from the child value dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetChildValue(string key)
        {
            if (ColumnModule.ChildrenValueDictionary != null && ColumnModule.ChildrenValueDictionary.ContainsKey(key))
            {
                return ColumnModule.ChildrenValueDictionary[key].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// If child value dictionary does not even contain the key, or the value is null/empty then dont even show the child.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ShowChild(string key)
        {
            return ColumnModule.ChildrenValueDictionary.ContainsKey(key) && ColumnModule.ChildrenValueDictionary[key].Active;
        }

        /// <summary>
        /// If in edit mode will add knockout data-bind for "visible" on key.Active, if not in edit mode will add style="display: none;" if key is not active.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ShowBasedOnActiveKey(string key)
        {
            if (InEditMode)
            {
                return String.Format("data-bind=\" visible: {0}.Active\"", key);
            }
            else if (!ShowChild(key))
            {
                return String.Format("style=\"{0}\"", "display: none;");
            }

            return string.Empty;
        }

        /// <summary>
        /// Will return either the data-bind necessary for knockout or the straight up key value for normal non editing mode.
        /// </summary>
        /// <param name="attributeKey"></param>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        public string GetHTMLAttributeValueFromKey(string attributeKey, string moduleKey)
        {
            if (InEditMode)   
            {
                return String.Format("data-bind=\"attr: {{{0}: {1}.Value}}\"", attributeKey, moduleKey);
            }
            else
            {
                return String.Format("{0}=\"{1}\"", attributeKey, GetChildValue(moduleKey));
            }
        }

        /// <summary>
        /// Determines if we should show the child, if so will create the html element with the necessary value.
        /// </summary>
        /// <param name="htmlElement"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ShowChildAndGetValue(string htmlElement, string key, string editType, string nonClassAttributes = "", string classAttribute = "")
        {
            if (InEditMode)
            {
                return CompanyCommons.Utility.RenderPartialViewToString(ControllerContext, "~/Views/PartialViews/KnockoutEditableField.ascx", new Editor.KnockoutEditableFieldModel(this, htmlElement, key, editType, nonClassAttributes, classAttribute));
            }
            else if (ShowChild(key))
            {
                if (htmlElement.ToUpper().Equals("IMG"))    
                {
                    return String.Format("<img src=\"{0}\" {1} {2} />", GetChildValue(key), nonClassAttributes, GetClassAttributeValue(classAttribute));
                }
                else if (htmlElement.ToUpper().Equals("ICON"))
                {
                    string[] Attributes = GetChildValue(key).Split('|');
                    //TODO: if the elementAttributes is a class or style this will mess up
                    return String.Format("<span class=\"{0} {1}\" style=\"color: {2};\" {3} ></span>", classAttribute, Attributes[0], Attributes[1], nonClassAttributes);
                }
                else if (htmlElement.ToUpper().Equals(Editor.SpecialHTMLElement.BootstrapButton.ToUpper()))
                {
                    string[] Attributes = GetChildValue(key).Split('|');
                    //TODO: if the elementAttributes is a class or style this will mess up
                    return String.Format("<a role=\"button\" class=\"btn {0} {1} {2}\" target=\"{3}\" href=\"{4}\" {5} >{6}</a>", classAttribute, Attributes[0], Attributes[1], Attributes[2], Attributes[3], nonClassAttributes, Attributes[4]);
                }
                else if (htmlElement.ToUpper().Equals(Editor.SpecialHTMLElement.ProductList.ToUpper()))
                {
                    return String.Format("<div class=\"chimera-product-list \"{0}\" product-list-url=\"{1}\" {2}></div>", classAttribute, GetChildValue(key), nonClassAttributes);
                }

                return String.Format("<{0} {1} {2} >{3}</{4}>", htmlElement, nonClassAttributes, GetClassAttributeValue(classAttribute), GetChildValue(key), htmlElement);
            }

            return string.Empty;
        }

        private string GetClassAttributeValue(string classNames)
        {
            return !classNames.Equals("") ? "class='" + classNames + "'" : "";
        }
    }
}