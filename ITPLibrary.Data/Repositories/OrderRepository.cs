namespace ITPLibrary.Data.Repositories
{
    public class OrderRepository : IOrderDataProvider
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Order> GetOrder(int OrderId)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == OrderId);
            if (order is null)
                return null;
            return order;
        }
        public async Task<Order> AddOrder(Order order)
        {
            try
            {

                await _appDbContext.Orders.AddAsync(order);

                await _appDbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var toUpdate = await _appDbContext.Orders.FindAsync(order.OrderId);

            if (toUpdate is null)
                return null;

            _appDbContext.Entry(toUpdate).CurrentValues.SetValues(order);

            await _appDbContext.SaveChangesAsync();

            return order;
        }

    }
}
