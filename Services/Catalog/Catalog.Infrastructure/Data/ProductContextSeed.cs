using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(p => true).Any();
            string path = Path.Combine("/app", "Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        productCollection.InsertOne(product);
                    }
                }
            }
        }
    }
}