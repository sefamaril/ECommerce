using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    // Author: Sefa Maril - Date: 04.08.2024
    // Description: This class represents the query to get a product by its ID.
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public string Id { get; set; }
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}
