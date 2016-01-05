using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Chimera.Entities;
using Chimera.Entities.Settings;
using Chimera.DataAccess;

namespace ChimeraDatabaseInitialize.Processors
{
    public class SettingGroupsProcessor : IProcessor
    {
        public string FilePath { get; set; }

        public SettingGroupsProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public void ProcessFile()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(FilePath);

            foreach (var Node in XmlDoc.DocumentElement.GetElementsByTagName("SettingGroup"))
            {
                XmlElement Element = (XmlElement)Node;

                SettingGroup SetGroup = new SettingGroup();

                SetGroup.GroupKey = Element.GetElementsByTagName("GroupKey")[0].InnerText;
                SetGroup.UserFriendlyName = Element.GetElementsByTagName("UserFriendlyName")[0].InnerText;
                SetGroup.Description = Element.GetElementsByTagName("Description")[0].InnerText;
                SetGroup.ParentCategory = (ParentCategoryType)Enum.Parse(typeof(ParentCategoryType), Element.GetElementsByTagName("ParentCategory")[0].InnerText);

                foreach (var ChildNode in Element.GetElementsByTagName("Setting"))
                {
                    XmlElement ChildElement = (XmlElement)ChildNode;

                    Setting Sett = new Setting();

                    Sett.Key = ChildElement.GetElementsByTagName("Key")[0].InnerText;
                    Sett.UserFriendlyName = ChildElement.GetElementsByTagName("UserFriendlyName")[0].InnerText;
                    Sett.Description = ChildElement.GetElementsByTagName("Description")[0].InnerText;
                    Sett.Value = ChildElement.GetElementsByTagName("Value")[0].InnerText;
                    Sett.EntryType = (DataEntryType) Enum.Parse(typeof(DataEntryType), ChildElement.GetElementsByTagName("EntryType")[0].InnerText);
                    Sett.DataEntryStaticPropertyKey = ChildElement.GetElementsByTagName("DataEntryStaticPropertyKey")[0].InnerText;

                    foreach (var ChildChildNode in ChildElement.GetElementsByTagName("SettingAttribute"))
                    {
                        XmlElement ChildChildElement = (XmlElement)ChildChildNode;

                        SettingAttribute SetAttr = new SettingAttribute();

                        SetAttr.Key = ChildChildElement.GetElementsByTagName("Key")[0].InnerText;
                        SetAttr.Value = ChildChildElement.GetElementsByTagName("Value")[0].InnerText;

                        Sett.SettingAttributeList.Add(SetAttr);
                    }

                    SetGroup.SettingsList.Add(Sett);
                }

                SettingGroupDAO.Save(SetGroup);
            }
        }
    }
}
