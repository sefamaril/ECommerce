using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static async Task SeedDatA(IMongoCollection<ProductType> typeCollection)
        {
            bool checkTypes = typeCollection.Find(t => true).Any();
            string path = Path.Combine("Data", "SeedData", "types");
            if (!checkTypes)
            {
                var typesData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        await typeCollection.InsertOneAsync(type);
                    }
                }
            }
        }
    }
}