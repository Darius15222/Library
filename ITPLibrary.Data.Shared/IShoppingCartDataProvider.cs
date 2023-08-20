namespace ITPLibrary.Data.Shared
{
    public interface IShoppingCartDataProvider
    {
        Task<List<ShoppingCartItem>> GetItems(Guid ShoppingCartId);
        Task<bool> ShoppingCartExists(Guid ShoppingCartId);
        
        Task<ShoppingCartItem> AddBookToShoppingCart(Guid ShoppingCartId, int BookId, int quantity);
        Task<ShoppingCart> DeleteSelectedShoppingCart(Guid ShoppingCartId);
        Task<ShoppingCart?> CreateShoppingCart(Guid ShoppingCartId);
    }
}
