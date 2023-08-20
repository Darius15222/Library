namespace ITPLibrary.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

	public ShoppingCartController(IShoppingCartService shoppingCartService)
	{
		_shoppingCartService = shoppingCartService;
	}


    [HttpGet("{shoppingCartId}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ShoppingCartItemDto>> GetShoppingCart([FromRoute] Guid shoppingCartId)
    {
        var shoppingCart = await _shoppingCartService.GetShoppingCartById(shoppingCartId);
        
        if(shoppingCart == null)
            return Ok("ShoppingCart created!");

        if (shoppingCart.ToList().Count == 0)
            return NotFound($"There are no items in the shoppingCart with the provided id!");

        return Ok(Json(shoppingCart));
    }


    [HttpPost("{shoppingCartId}/{bookId}/{quantity}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingCartItemDto>> AddBookToShoppingCart([FromRoute] Guid shoppingCartId, [FromRoute] int bookId, [FromRoute] int quantity)
    {
        if (quantity == 0)
            return BadRequest("Quantity can not be 0!");

        var result = await _shoppingCartService.AddBookToShoppingCart(shoppingCartId, bookId, quantity);
        if (result is null)
            return BadRequest($"Failed to add book with the provided details to the shoppingcart. Please try again!");
        return Ok(Json(result));
    }


    [HttpDelete("Delete/{shoppingCartId}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteShoppingCart([FromRoute] Guid shoppingCartId)
    {
        //var cartToDelete = await _shoppingCartService.GetShoppingCartById(shoppingCartId);

        //if (cartToDelete == null)
        //    return NotFound($"ShoppingCart with id {shoppingCartId} not found! No delete performed.");

        var result = await _shoppingCartService.DeleteShoppingCartAndItems(shoppingCartId);

        if (result == null)
            return NotFound($"ShoppingCart with id {shoppingCartId} not found! No delete performed.");

        return Ok(Json(result));
    }
}

