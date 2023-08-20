using ITPLibrary.Data.Shared.Dtos.ShoppingCartDto;
using ITPLibrary.Data.Shared.Dtos.ShoppingCartItemDto;

namespace ITPLibrary.Api.Core.Services.Implementations;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartDataProvider _shoppingCartDataProvider;
    private readonly IBookDataProvider _bookDataProvider;
    private readonly IMapper _mapper;

    public ShoppingCartService(IShoppingCartDataProvider shoppingCartDataProvider, IMapper mapper, IBookDataProvider bookDataProvider)
    {
        _shoppingCartDataProvider = shoppingCartDataProvider;
        _mapper = mapper;
        _bookDataProvider = bookDataProvider;
    }

    public async Task<List<ShoppingCartItemDto>> GetShoppingCartById(Guid ShoppingCartId)
    {
        var exists = await _shoppingCartDataProvider.ShoppingCartExists(ShoppingCartId);
        if (!exists)
        { 
            await _shoppingCartDataProvider.CreateShoppingCart(ShoppingCartId);
            return null;
        }
        else
        {
            var items = await _shoppingCartDataProvider.GetItems(ShoppingCartId);
            var itemsDto = _mapper.Map<List<ShoppingCartItemDto>>(items);
            return itemsDto;
        }
    }

    public async Task<ShoppingCartItemDto> AddBookToShoppingCart(Guid ShoppingCartId, int BookId, int quantity)
    {
        var exists = await _shoppingCartDataProvider.ShoppingCartExists(ShoppingCartId);
        if (!exists)
            return null;

        var book = await _bookDataProvider.GetById(BookId);
        
        if (book == null)
            return null;
            
        var cart =  await _shoppingCartDataProvider.AddBookToShoppingCart(ShoppingCartId, BookId, quantity);

        var cartDto = _mapper.Map<ShoppingCartItemDto>(cart);

        return cartDto;
    }

    public async Task<ShoppingCartDto> DeleteShoppingCartAndItems(Guid ShoppingCartId)
    {
        var exists = await _shoppingCartDataProvider.ShoppingCartExists(ShoppingCartId);

        if (exists)
        {
            var result = await _shoppingCartDataProvider.DeleteSelectedShoppingCart(ShoppingCartId);

            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(result);

            return shoppingCartDto;
        }
        else return null;

    }


}
