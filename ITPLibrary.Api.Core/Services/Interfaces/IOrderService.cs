using ITPLibrary.Data.Shared.Dtos.OrderDto;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderById(int orderId);
        Task<OrderDto> AddOrder(OrderDto orderDto);
        Task<OrderDto> UpdateOrder(OrderDto orderDto);
    }
}
