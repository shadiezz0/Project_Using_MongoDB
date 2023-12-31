﻿using Project_Using_MongoDB.Models;

namespace Project_Using_MongoDB
{
    public interface ICategoryRepos
    {
        Task<IEnumerable<Category>> GetAllAsyc();
        Task<Category> GetById(string id);
        Task CreateAsync(Category category);
        Task UpdateAsync(string id, Category category);
        Task DeleteAysnc(string id);
    }
}