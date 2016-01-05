using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chimera.Entities.Admin
{
    public class AdminUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Unique username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Their email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashed password.
        /// </summary>
        public string Hashed_Password { get; set; }

        /// <summary>
        /// List of roles this user owns.
        /// </summary>
        public List<string> RoleList { get; set; }

        /// <summary>
        /// Datetime the user was last updated.
        /// </summary>
        public DateTime ModifiedDateUTC { get; set; }

        /// <summary>
        /// Datetime of when the user last logged into the admin website.
        /// </summary>
        public DateTime LastLoginDateUTC { get; set; }

        /// <summary>
        /// Is this an active admin user?  if not active they can't even log in.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdminUser()
        {
            Id = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Hashed_Password = string.Empty;
            RoleList = new List<string>();
            ModifiedDateUTC = DateTime.MinValue;
            LastLoginDateUTC = DateTime.MinValue;
            Active = false;
        }
    }
}
