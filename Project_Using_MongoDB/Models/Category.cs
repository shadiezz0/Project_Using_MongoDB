using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Project_Using_MongoDB.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? CategoryName { get; set; }
    }
}
