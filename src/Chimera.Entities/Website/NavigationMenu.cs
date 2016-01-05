using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CompanyCommons.Entities;

namespace Chimera.Entities.Website
{
    public class NavigationMenu
    {
        public const string MAIN_NAVIGATION_MENU = "MainNavigationMenu";

        /// <summary>
        /// MongoDB ID
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Where does this navigation menu go?
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// A client friendly name for the group of links
        /// </summary>
        public string UserFriendlyName { get; set; }

        /// <summary>
        /// The children nav links
        /// </summary>
        public List<NavigationMenuLink> ChildNavLinks { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public NavigationMenu()
        {
            Id = string.Empty;
            KeyName = string.Empty;
            UserFriendlyName = string.Empty;
            ChildNavLinks = new List<NavigationMenuLink>();
        }

        /// <summary>
        /// Validate this navigation menu before saving, if return list of user messages is empty then can save
        /// </summary>
        /// <returns></returns>
        public List<WebUserMessage> Validate()
        {
            List<WebUserMessage> WebUserMessageList = new List<WebUserMessage>();

            string FailedType = WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE;

            if (string.IsNullOrWhiteSpace(KeyName))
            {
                WebUserMessageList.Add(new WebUserMessage("Key Name field can't be empty or whitespace.", FailedType));
            }

            if (string.IsNullOrWhiteSpace(UserFriendlyName))
            {
                WebUserMessageList.Add(new WebUserMessage("User Friendly Name field can't be empty or whitespace.", FailedType));
            }

            return WebUserMessageList;
        }
    }
}
