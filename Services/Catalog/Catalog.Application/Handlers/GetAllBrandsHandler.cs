using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    // Author: Sefa Maril - Date: 04.08.2024
    // Description: This class handles the GetAllBrandsQuery request to retrieve all brands.
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetAllBrands();
            //var brandResponseList = _mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brandList.ToList());
            var brandResponseList = ProductMapper.Mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brandList.ToList());
            return brandResponseList;
        }
    }
}

//Modularity: Commands and queries are managed separately, making the code more modular.
//Testability: Separately managed commands and queries make unit testing easier.
//Reduced Tight Coupling: Reduces dependencies between different functions, making the code more flexible.
//Readability: Improves code readability as each function is defined separately.