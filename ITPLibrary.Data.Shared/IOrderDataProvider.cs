namespace ITPLibrary.Data.Shared
{
    public interface IOrderDataProvider
    {
        Task<Order> GetOrder(int OrderId);
        Task<Order> AddOrder(Order orderDto);
        Task<Order> UpdateOrder(Order orderDto);
    }
}
