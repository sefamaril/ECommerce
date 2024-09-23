using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Ordering Database:{typeof(OrderContext).Name} seeded!!!");
        }
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "sefam",
                FirstName = "Sefa",
                LastName = "Maril",
                EmailAddress = "sefamaril17@gmail.com",
                AddressLine = "Istanbul",
                Country = "Turkey",
                TotalPrice = 750,
                State = "Istanbul",
                ZipCode = "34000",
                
                CardName = "Visa",
                CardNumber = "1234567812345678",
                CreatedBy = "Sefa",
                Expiration = "12/30",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "Sefa",
                LastModifiedDate = new DateTime(),
            }
        };
    }
}