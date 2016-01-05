using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Admin
{
    public class AdminUserRole
    {
        /// <summary>
        /// Mongo PK.
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of what the role means.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdminUserRole()
        {
            Id = ObjectId.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
