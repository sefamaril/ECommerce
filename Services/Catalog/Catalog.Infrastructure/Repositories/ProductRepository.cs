using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    // Author: Sefa Maril - Date: 04.08.2024
    // Description: This class implements the IProductRepository, IBrandRepository, and ITypesRepository interfaces to manage product, brand, and type-related operations.
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }
        public async Task<IEnumerable<Product>> GetProductByBrand(string brandName)
        {
            return await _context.Products
                         .Find(p => p.Brands.Name.ToLower() == brandName.ToLower())
                         .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products
                         .Find(p => p.Name.ToLower() == name.ToLower())
                         .ToListAsync();
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            var result = await _context.Brands.Find(b => true).ToListAsync();
            return await _context.Brands.Find(b => true).ToListAsync();
        }
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context.Types.Find(t => true).ToListAsync();
        }
    }
}
