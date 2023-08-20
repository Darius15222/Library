namespace ITPLibrary.Data.Repositories;

public class ShoppingCartRepository : IShoppingCartDataProvider
{
    private readonly AppDbContext _appDbContext;

    public ShoppingCartRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ShoppingCartItem>> GetItems(Guid ShoppingCartId)
    {
        var shoppingCart = await _appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId.Equals(ShoppingCartId)).ToListAsync();
        if (shoppingCart is null)
            return null;
        return shoppingCart;
    }

    public async Task<bool> ShoppingCartExists(Guid ShoppingCartId)
    {
        var cart = await _appDbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.ShoppingCartId == ShoppingCartId);
        if (cart is null)
            return false;
        return true;
    }
    public async Task<ShoppingCart> CreateShoppingCart(Guid ShoppingCartId)
    {
        var shoppingCart = new ShoppingCart()
        {
            ShoppingCartId = ShoppingCartId,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _appDbContext.ShoppingCarts.AddAsync(shoppingCart);
        await _appDbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ShoppingCartItem> AddBookToShoppingCart(Guid shoppingCartId, int bookId, int quantity)
    {
        try
        {
            var existingCartItem = await _appDbContext.ShoppingCartItems
                .FirstOrDefaultAsync(item => item.ShoppingCartId == shoppingCartId && item.BookId == bookId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                await _appDbContext.SaveChangesAsync();
                return existingCartItem;
            }
            else
            {
                var shoppingCart = new ShoppingCartItem()
                {
                    ShoppingCartId = shoppingCartId,
                    BookId = bookId,
                    Quantity = quantity,
                };

                var cart = await _appDbContext.ShoppingCartItems.AddAsync(shoppingCart);
                await _appDbContext.SaveChangesAsync();

                var item = cart.Entity;
                return item;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<ShoppingCart> DeleteSelectedShoppingCart(Guid ShoppingCartId)
    {
        try
        {
            var shoppingCartItems = await _appDbContext.ShoppingCartItems.
                FirstOrDefaultAsync(x => x.ShoppingCartId == ShoppingCartId);

            while (shoppingCartItems != null)
            {
                _appDbContext.ShoppingCartItems.Remove(shoppingCartItems);
                await _appDbContext.SaveChangesAsync();

                shoppingCartItems = await _appDbContext.ShoppingCartItems.
                FirstOrDefaultAsync(x => x.ShoppingCartId == ShoppingCartId);
            }

            var shoppingCart = await _appDbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.ShoppingCartId.Equals(ShoppingCartId));

            if (shoppingCart is null)
                return null;

            _appDbContext.ShoppingCarts.Remove(shoppingCart);
            await _appDbContext.SaveChangesAsync();

            return shoppingCart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
