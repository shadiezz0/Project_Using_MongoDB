using Project_Using_MongoDB.Models;

namespace Project_Using_MongoDB
{
    public interface IProductRepos
    {
        Task<IEnumerable<Product>> GetAllAsyc();
        Task<Product> GetById(string id);
        Task CreateAsync(Product Product);
        Task UpdateAsync(string id, Product Product);
        Task DeleteAysnc(string id);
    }
}