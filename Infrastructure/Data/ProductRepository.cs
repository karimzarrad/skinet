using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext context) : IProductRepository
    {
        public void Addproduct(Product product)
        {
            context.Products.Add(product);  
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<IReadOnlyList<string>> GetBrandsasync()
        {
            return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int id)
        {
             return await  context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string?brand,string?type,string? sort)
        {
            var query=context.Products.AsQueryable();
            if(!string.IsNullOrWhiteSpace(brand) )
                query = query.Where(x => x.Brand == brand);

            if(!string.IsNullOrWhiteSpace(type) )
                query=query.Where(x => x.Type == type);
           
                query = sort switch
                {
                    "PriceAsc" => query.OrderBy(x => x.Price),
                    "PriceDsc" => query.OrderByDescending(x => x.Price),
                    _ => query.OrderBy(x => x.Name)
                };
            
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesasync()
        {
            return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return context.Products.Any(x=>x.Id==id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync()>0;
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Entry(product).State = EntityState.Modified;
        }
    }
}
