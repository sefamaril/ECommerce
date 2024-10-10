using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers;

public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckOutOrderCommandHandler> _logger;

    public CheckOutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckOutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var generateOrder = await _orderRepository.AddAsync(orderEntity);
        _logger.LogInformation($"Order {generateOrder.Id} successfully created");
        return generateOrder.Id;
    }
}