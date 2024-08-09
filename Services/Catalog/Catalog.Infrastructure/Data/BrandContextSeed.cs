using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        // Author: Sefa Maril - Date: 03.08.2024
        // Description: Description: This file and other ContextSeed contains the seed data operations for initializing the database with predefined data
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrands = brandCollection.Find(b => true).Any();
            string path = Path.Combine("/app", "Data", "SeedData", "brands.json");
            if (!checkBrands)
            {
                var brandsData = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var brand in brands)
                    {
                        brandCollection.InsertOne(brand);
                    }
                }
            }
        }
    }
}