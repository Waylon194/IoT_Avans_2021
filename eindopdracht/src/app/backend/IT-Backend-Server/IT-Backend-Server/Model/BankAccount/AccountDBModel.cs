using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWSN_Backend_Server.Models.ModelInstances;
using System.Runtime.Serialization;

namespace IWSN_Backend_Server.Models
{
    public class AccountDBModel
    {
        /// <summary>
        /// <- Remark -> - When using the System.Text.Json make sure the attributes are reachable with { get; set; }!
        /// </summary>
        public AccountDBModel() { }

        // This is used for marking the MongoDB assigned unique id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Internal user class
        [BsonElement("User")]
        public UserDBModel User { get; set; }

        // balance available of the user
        [BsonElement("Balance")]
        public int Balance { get; set; }
    }
}
