using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportsAPI.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id_Origin { get; set; } = ObjectId.GenerateNewId().ToString();
    }
}
