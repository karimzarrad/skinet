using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeeding
    {
        public static async Task seedasync(StoreContext context)
        {
            var productlist = await File.ReadAllTextAsync("../Infrastructure/Data/SeedingData/products.json");
            var products=JsonSerializer.Deserialize<List<Product>>(productlist);
            if (products==null)
            {
                return;
            }
            await context.Products.AddRangeAsync(products);
        }
    }
}
