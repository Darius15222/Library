using ITPLibrary.Data.Shared.Dtos.OrderDto;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDataProvider _orderDataProvider;
        private readonly IMapper _mapper;
        public OrderService(IOrderDataProvider orderDataProvider, IMapper mapper)
        {
            _orderDataProvider = orderDataProvider;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            var order =  await _orderDataProvider.GetOrder(orderId);
            
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<OrderDto> AddOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            var addedOrder =  await _orderDataProvider.AddOrder(order);

            return _mapper.Map<OrderDto>(addedOrder);
        }

        public async Task<OrderDto> UpdateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var updatedOrder =  await _orderDataProvider.UpdateOrder(order);

            return _mapper.Map<OrderDto>(updatedOrder);
        }
    }
}
