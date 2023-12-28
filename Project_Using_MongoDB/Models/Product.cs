using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Project_Using_MongoDB.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }


        // not store in db, if you pass a null value to it, so make sure make it null before passing to db
        [BsonIgnoreIfNull]
        public string? CategoryName { get; set; }
    }
}
