using MongoDB.Bson;

namespace MyMassage.Backend.Models
{
    public abstract class BusinessEntityBase
    {
        public ObjectId Id { get; set; }
    }
}
