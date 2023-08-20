namespace ITPLibrary.Data.DataProvider.DataProviders
{
    public class ShoppingCartDataProvider : IShoppingCartDataProvider
    {
        private readonly SqlConnector _sqlConnector;

        public ShoppingCartDataProvider(IConfiguration configuration)
        {
            _sqlConnector = new SqlConnector(configuration[Constants.DatabaseConnectionString]);
        }

        public async Task<List<ShoppingCartItem>> GetItems(Guid ShoppingCartId)
        {
            var sqlQuery = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryAsync<ShoppingCartItem>(sqlQuery, new { ShoppingCartId });
                return result.ToList();
            }
        }

        public async Task<ShoppingCartItem> AddBookToShoppingCart(Guid ShoppingCartId, int BookId, int quantity)
        {
            var existingCartItem = await GetCartItemByShoppingCartAndBookId(ShoppingCartId, BookId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                await UpdateCartItem(existingCartItem);
                return existingCartItem;
            }

            var sqlQuery = "INSERT INTO ShoppingCartItems (ShoppingCartId, BookId, Quantity) "
                + "VALUES (@ShoppingCartId, @BookId, @Quantity);"

                + "SELECT * FROM ShoppingCartItems WHERE ShoppingCartItemId = SCOPE_IDENTITY()";

            var parameters = new
            {
                ShoppingCartId,
                BookId,
                Quantity = quantity
            };

            using (var connection = _sqlConnector.CreateConnection())
            {
                var insertedItem = await connection.QuerySingleOrDefaultAsync<ShoppingCartItem>(sqlQuery, parameters);
                return insertedItem;
            }
        }

        private async Task<ShoppingCartItem> GetCartItemByShoppingCartAndBookId(Guid ShoppingCartId, int BookId)
        {
            var sqlQuery = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId AND BookId = @BookId";

            var parameters = new
            {
                ShoppingCartId,
                BookId
            };

            using (var connection = _sqlConnector.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<ShoppingCartItem>(sqlQuery, parameters);
                return item;
            }
        }

        private async Task UpdateCartItem(ShoppingCartItem cartItem)
        {
            var sqlQuery = "UPDATE ShoppingCartItems SET Quantity = @Quantity WHERE ShoppingCartItemId = @ShoppingCartItemId";

            var parameters = new
            {
                cartItem.Quantity,
                cartItem.ShoppingCartItemId
            };

            using (var connection = _sqlConnector.CreateConnection())
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }




        public async Task<ShoppingCart> DeleteSelectedShoppingCart(Guid ShoppingCartId)
        {
            var sqlQuery = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            var sqlQuery1 = "DELETE FROM ShoppingCarts WHERE ShoppingCartId = @ShoppingCartId";
            
            var sqlQuery2 = "SELECT * FROM ShoppingCarts WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var cart = await connection.QuerySingleOrDefaultAsync<ShoppingCart>(sqlQuery1, new { ShoppingCartId });

                if (cart == null)
                    return null;

                await connection.ExecuteAsync(sqlQuery, new { ShoppingCartId });
                await connection.ExecuteAsync(sqlQuery1, new { ShoppingCartId });

                return cart;
            }
        }


        public async Task<bool> ShoppingCartExists(Guid ShoppingCartId)
        {
            var sqlQuery = "SELECT TOP 1 1 FROM ShoppingCarts WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _sqlConnector.CreateConnection())
            {
               var result = await connection.QueryAsync<int?>(sqlQuery, new { ShoppingCartId });

                return result.Any();
            }
        }

        public async Task<ShoppingCart> CreateShoppingCart(Guid ShoppingCartId)
        {
            var sqlQuery = "INSERT INTO ShoppingCarts(ShoppingCartId, CreatedAt) VALUES(@ShoppingCartId, @createdAt)";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryAsync<ShoppingCart>(sqlQuery, new { ShoppingCartId, createdAt = DateTime.UtcNow });
                return result?.FirstOrDefault();
            }
        }
    }
}
