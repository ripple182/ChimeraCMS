using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Chimera.Entities.Website;
using Chimera.DataAccess;

namespace ChimeraDatabaseInitialize.Processors
{
    public class NavigationMenuProcessor : IProcessor
    {
        public string FilePath { get; set; }

        public NavigationMenuProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public void ProcessFile()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(FilePath);

            foreach (var Node in XmlDoc.DocumentElement.GetElementsByTagName("NavigationMenu"))
            {
                XmlElement Element = (XmlElement) Node;

                NavigationMenu NavMenu = new NavigationMenu();

                NavMenu.KeyName = Element.GetElementsByTagName("KeyName")[0].InnerText;
                NavMenu.UserFriendlyName = Element.GetElementsByTagName("UserFriendlyName")[0].InnerText;

                ProcessChildLinks(Element, NavMenu.ChildNavLinks);

                NavigationMenuDAO.Save(NavMenu);
            }
        }

        private void ProcessChildLinks(XmlElement element, List<NavigationMenuLink> childLinkList)
        {
            foreach (var ChildNode in element.GetElementsByTagName("NavigationMenuLink"))
            {
                XmlElement ChildElement = (XmlElement)ChildNode;

                NavigationMenuLink NavLink = new NavigationMenuLink();

                NavLink.Text = ChildElement.GetElementsByTagName("Text")[0].InnerText;
                NavLink.ChimeraPageUrl = ChildElement.GetElementsByTagName("ChimeraPageUrl")[0].InnerText;
                NavLink.LinkAction = ChildElement.GetElementsByTagName("LinkAction")[0].InnerText;
                NavLink.RealUrl = ChildElement.GetElementsByTagName("RealUrl")[0].InnerText;

                ProcessChildLinks(ChildElement, NavLink.ChildNavLinks);

                childLinkList.Add(NavLink);
            }
        }
    }
}
