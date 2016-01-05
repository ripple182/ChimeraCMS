using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;


namespace ChimeraWebsite.Models.Editor
{
    public static class CacheInitializer  
    {
        /// <summary>
        /// Initialize needed content into the application cache.
        /// </summary>
        /// <param name="context"></param>
        public static void InitializeEditorCache(ControllerContext controllerContext, HttpContextBase context)
        {
            Dictionary<string, string> DefaultTokenAppSettings = DefaultTokenValues.GetDictionary(controllerContext, context);

            HashSet<string> Categories = new HashSet<string>();

            //add template specific modules
            List<string> FilePaths = Directory.GetFiles(context.Server.MapPath(String.Format(@"~\Templates\{0}\Views\ModuleViews\", ChimeraTemplate.TemplateName))).ToList();
            
            //add common modules as well
            FilePaths.AddRange(Directory.GetFiles(context.Server.MapPath(@"~\Views\PartialViews\CommonModuleViews\")).ToList());

            foreach (var File in FilePaths)
            {
                ColumnModuleType ColumnModuleType = new ColumnModuleType();

                List<string> CategoryList = new List<string>();

                ColumnModuleType.ColumnModuleModel.ColumnModule.ModuleTypeName = System.IO.Path.GetFileNameWithoutExtension(File);

                System.IO.StreamReader sreader = new System.IO.StreamReader(File);

                string Line = string.Empty;

                while ((Line = sreader.ReadLine()) != null)
                {
                    Line.Trim();

                    foreach (var Token in DefaultTokenAppSettings)
                    {
                        Line = Line.Replace(Token.Key, Token.Value);
                    }

                    if (Line.Contains("[#Display_Name#]"))
                    {
                        ColumnModuleType.DisplayName = GetValueFromModuleViewLine(Line);
                    }
                    else if (Line.Contains("[#Display_Description#]"))
                    {
                        ColumnModuleType.DisplayDescription = GetValueFromModuleViewLine(Line);
                    }
                    else if (Line.Contains("[#Parent_Categories#]"))
                    {
                        ColumnModuleType.Categories = new HashSet<string>(GetValueFromModuleViewLine(Line).Split(',').Select(category => category.Trim()));
                    }
                    else if (Line.Contains("[#AdditionalClassNameList#]"))
                    {
                        ColumnModuleType.ColumnModuleModel.ColumnModule.AdditionalClassNames = GetValueFromModuleViewLine(Line);
                    }
                    else if (Line.Contains("[#Default_Value_"))
                    {
                        ColumnModuleType.ColumnModuleModel.ColumnModule.ChildrenValueDictionary.Add(GetDefaultValueKeyFromModuleViewLine(Line), new Chimera.Entities.Page.Module.ColumnModuleChild(GetValueFromModuleViewLine(Line)));
                    }
                    else if (Line.Contains("[#Default_Inactive_")) 
                    {
                        ColumnModuleType.ColumnModuleModel.ColumnModule.ChildrenValueDictionary[GetInactiveKeyFromModuleViewLine(Line)].Active = false;
                    } 
                } 

                sreader.Close();

                foreach (var Category in ColumnModuleType.Categories)
                {
                    ColumnModuleTypeCategory.AddNew(context, Category);
                }

                ColumnModuleType.AddNew(context, ColumnModuleType);
            }
        }

        /// <summary>
        /// Get the default value key from the partial view meta data.  (i.e.) [#Default_Value_Title#] would return "Title".
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetDefaultValueKeyFromModuleViewLine(string line)
        {
            int idx = line.IndexOf("[#Default_Value_");
            int idx2 = line.IndexOf("#]=");

            if (idx2 > 0)
            {
                return line.Substring(idx + 16, idx2 - (idx + 16)).Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the KEY of the child component that should be inactive.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetInactiveKeyFromModuleViewLine(string line)
        {
            int idx = line.IndexOf("[#Default_Inactive_");
            int idx2 = line.IndexOf("#]");

            if (idx2 > 0)
            {
                return line.Substring(idx + 19, idx2 - (idx + 19)).Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// Simply get the value from a line that contains an equal sign.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetValueFromModuleViewLine(string line)
        {
            int idx = line.IndexOf("=");

            if (idx > 0)
            {
                return line.Substring(idx + 1, line.Length - idx - 1).Trim();
            }

            return string.Empty;
        }
    }
}