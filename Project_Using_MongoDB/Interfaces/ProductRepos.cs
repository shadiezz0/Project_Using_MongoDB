using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Project_Using_MongoDB.Models;

namespace Project_Using_MongoDB.Interfaces
{
    public class ProductRepos : IProductRepos
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IOptions<MongoDBContext> _db;

        public ProductRepos(IOptions<MongoDBContext> dbSettings)
        {
            _db = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>(dbSettings.Value.ProductsCollectionName);
        }

        public async Task<IEnumerable<Product>> GetAllAsyc()
        {
            // Define the pipeline for the aggregation query
            var pipeline = new BsonDocument[]
            {
              new BsonDocument("$lookup", new BsonDocument
              {
                { "from", "CategoryCollection" },
                { "localField", "CategoryId" },// join 2 property
                { "foreignField", "_id" },
                { "as", "product_category" } // result store in this table
              }),
              new BsonDocument("$unwind", "$product_category"), // convert array to flatten the array
              new BsonDocument("$project", new BsonDocument
              {
                { "_id", 1 },
                { "CategoryId", 1},
                { "ProductName",1 },
                { "CategoryName", "$product_category.CategoryName" }
              })
            };

            var results = await _productCollection.Aggregate<Product>(pipeline).ToListAsync();
            return results;
        }

        //public async Task<List<Product>> GetAllProductsAsync()
        //{
        //    var pipeline = _productCollection.Aggregate()
        //        .Lookup(
        //            foreignCollection: _categories,
        //            localField: p => p.CategoryId,
        //            foreignField: c => c.Id,
        //            @as: (Product p) => p.Category // Update 'Category' property in Product to hold the category data
        //        )
        //        .ToList();

        //    return await pipeline;
        //}

        public async Task<Product> GetById(string id) =>
            await _productCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product Product) =>
            await _productCollection.InsertOneAsync(Product);

        public async Task UpdateAsync(string id, Product Product) =>
            await _productCollection
            .ReplaceOneAsync(a => a.Id == id, Product);

        public async Task DeleteAysnc(string id) =>
            await _productCollection.DeleteOneAsync(a => a.Id == id);

    }
}
