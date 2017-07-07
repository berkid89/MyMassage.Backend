using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyMassage.Backend.Models
{
    public abstract class BusinessEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
