namespace ITPLibrary.Data.DataProvider.DataProviders
{
    public class OrderDataProvider : IOrderDataProvider
    {

        private readonly SqlConnector _sqlConnector;
        public OrderDataProvider(IConfiguration configuration)
        {
            _sqlConnector = new SqlConnector(configuration[Constants.DatabaseConnectionString]);
        }

        public async Task<Order> GetOrder(int OrderId)
        {
            var sqlQuery = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var order =  await connection.QueryFirstOrDefaultAsync<Order>(sqlQuery, new { OrderId });
                if (order == null)
                    return null;

                return order;
            }
        }
        
        public async Task<Order> AddOrder(Order order)
        {
            var sqlQuery = "INSERT INTO Orders (FirstName, LastName, Email, Billing_Address, Billing_Country, Billing_Number, " +
                "Delivery_Address, Delivery_Country, Delivery_Number, PaymentType, OrderStatus, OrderPlaced, DeliveryDate, Observations) " +
            
                "VALUES (@FirstName, @LastName, @Email, @Billing_Address, @Billing_Country, @Billing_Number, @Delivery_Address, @Delivery_Country, " +
            "@Delivery_Number, @PaymentType, @OrderStatus, @OrderPlaced, @DeliveryDate, @Observations)";

            using (var connection = _sqlConnector.CreateConnection())
            {
                await connection.ExecuteAsync(sqlQuery, order);
                return order;
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var sqlQuery = "UPDATE Orders SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Billing_Address = @Billing_Address, " +
                "Billing_Country = @Billing_Country, Billing_Number = @Billing_Number, Delivery_Address = @Delivery_Address, " +
                "Delivery_Country = @Delivery_Country, Delivery_Number = @Delivery_Number, PaymentType = @PaymentType, " +
                "OrderStatus = @OrderStatus, OrderPlaced = @OrderPlaced, DeliveryDate = @DeliveryDate, " +
                "Observations = @Observations WHERE OrderId = @OrderId";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sqlQuery, order);

                return rowsAffected != 0 ? order : null;
            }
        }
        
    }
}
