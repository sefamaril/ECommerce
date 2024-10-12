using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{

    public class CheckOutOrderCommandV2Handler : IRequestHandler<CheckOutOrderCommandV2, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckOutOrderCommandV2Handler> _logger;

        public CheckOutOrderCommandV2Handler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckOutOrderCommandV2Handler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CheckOutOrderCommandV2 request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var generateOrder = await _orderRepository.AddAsync(orderEntity);
            _logger.LogInformation($"Order {generateOrder.Id} successfully created");
            return generateOrder.Id;
        }
    }
}