

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiDemo.model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("user")]
        public string acount { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("acesstoken")]
        public string acesstoken { get; set; }
        [BsonElement("date")]
        public BsonDateTime date { get; set; }
    }
}
