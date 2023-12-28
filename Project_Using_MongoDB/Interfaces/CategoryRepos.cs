using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Project_Using_MongoDB.Models;

namespace Project_Using_MongoDB.Interfaces
{
    public class CategoryRepos : ICategoryRepos
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IOptions<MongoDBContext> _db;

        public CategoryRepos(IOptions<MongoDBContext> db)
        {
            _db = db;
            var mongoClient = new MongoClient(db.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(db.Value.DatabaseName);
            _categoryCollection = mongoDatabase.GetCollection<Category>(db.Value.CategoriesCollectionName);
        }

        public async Task<IEnumerable<Category>> GetAllAsyc()
        {
            return await _categoryCollection.Find(_ => true).ToListAsync();//mean get all cat from DB
        }

        public async Task<Category> GetById(string id)
        {
            return await _categoryCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Category Category) =>
            await _categoryCollection.InsertOneAsync(Category);

        public async Task UpdateAsync(string id, Category Category) =>
            await _categoryCollection.ReplaceOneAsync(a => a.Id == id, Category);

        public async Task DeleteAysnc(string id) =>
            await _categoryCollection.DeleteOneAsync(a => a.Id == id);

    }
}
