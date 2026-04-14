using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<IReadOnlyList<Product>> GetProductsAsync(string?brand,string?type,string? sort);
        Task<Product?>GetProductAsync(int id);
        public Task<IReadOnlyList<string>> GetBrandsasync();
        public Task<IReadOnlyList<string>> GetTypesasync();
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void Addproduct(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
    }
}
