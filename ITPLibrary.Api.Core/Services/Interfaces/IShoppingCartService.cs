using ITPLibrary.Data.Shared.Dtos.ShoppingCartDto;
using ITPLibrary.Data.Shared.Dtos.ShoppingCartItemDto;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartItemDto>> GetShoppingCartById(Guid ShoppingCartId);
        Task<ShoppingCartItemDto> AddBookToShoppingCart(Guid ShoppingCartId, int BookId, int quantity);
        Task<ShoppingCartDto> DeleteShoppingCartAndItems(Guid ShoppingCartId);
    }
}
