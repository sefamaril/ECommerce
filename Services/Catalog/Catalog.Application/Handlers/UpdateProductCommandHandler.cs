using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.UpdateProduct(new Product
            {
                Id = request.Id,
                Name = request.Name,
                Summary = request.Summary,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Brands = request.Brands,
                Types = request.Types,
            });
            return true;
        }
    }
}