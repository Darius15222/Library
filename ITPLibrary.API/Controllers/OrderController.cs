namespace ITPLibrary.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<OrderDto>> GetOrder([FromRoute]int orderId)
    {
        var order = await _orderService.GetOrderById(orderId);
        if (order == null)
            return NotFound($"Order with the id {orderId} not found!");
        return Ok(Json(order));
    }


    [HttpPost("AddOrder")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<OrderDto>> AddOrder([FromBody]OrderDto orderDto)
    {
        var newOrder = await _orderService.AddOrder(orderDto);
        
        if(newOrder is null)
            return BadRequest($"Failed to add order with the provided details. Please try again!");
        
        return CreatedAtAction(nameof(GetOrder), new { orderId = newOrder.OrderId }, newOrder);

    }


    [HttpPut("UpdateOrder/{orderId}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<OrderDto>> UpdateOrder([FromRoute] int orderId, [FromBody] OrderDto orderDto)
    {
        orderDto.OrderId = orderId;
        var result = await _orderService.UpdateOrder(orderDto);

        if(result is null)
            return NotFound($"Order with id {orderId} not found! No update performed.");

        return Ok(Json("Order updated!"));
    }
}
