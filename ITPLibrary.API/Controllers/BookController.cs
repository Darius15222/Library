namespace ITPLibrary.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet("GetAllBooks")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<BookDto>>> Get()
    {
        var result = await _bookService.GetAllBooks();
        if (result is null)
            return NotFound($"There are no books!");
        return Ok(result);
    }


    [HttpGet("GetAllPromotedBooks")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<PromotedBooksDto>>> GetPromotedBooks()
    {
        var result = await _bookService.GetPromotedBooks();
        if (result is null)
            return NotFound($"There are no promoted books!");
        return Ok(result);
    }


    [HttpGet("GetBestBooksRecentlyAdded")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<BookSellerDto>>> GetBestBooksRecentlyAdded()
    {
        var result = await _bookService.GetBestBooksRecentlyAdded();
        if (result is null)
            return NotFound($"There are no books recently added!");
        return Ok(result);
    }


    [HttpGet("GetBookById/{id}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BookDto>> GetSingle([FromRoute] int id)
    {
        var result =  await _bookService.GetBookById(id);
        if(result == null)
            return NotFound($"Book with id {id} not found");
        return Ok(result);
    }


    [HttpGet("GetBookDetails/{id}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BookDetailsDto>> GetBookDetails([FromRoute]int id)
    {
        var result = await _bookService.GetBookDetails(id);
        if (result is null)
            return NotFound($"Book with id {id} not found!");
        return Ok(result);
    }


    [HttpPost("AddBook")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BookDto>> AddBook([FromBody] BookDto newBookDto)
    {
        var newBook = await _bookService.AddBook(newBookDto);
        if (newBook is null)
            return BadRequest($"Failed to add book with the provided details. Please try again!");

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }


    [HttpPut("UpdateBook/{id}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BookDto>> UpdateBook([FromRoute]int id, [FromBody]BookDto updatedBook)
    {
        updatedBook.Id = id;
        var result = await _bookService.UpdateBook(updatedBook);
        if (result is null)
            return NotFound($"Book with id {id} not found! No update performed.");

        return Ok(result);
    }


    [HttpDelete("DeleteBook/{id}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Delete([FromRoute]int id)
    {
        var bookToDelete = await _bookService.DeleteBook(id);

        if (bookToDelete == null)
            return NotFound($"Book with id {id} not found! No delete performed.");

        return Ok(bookToDelete);
    }
}
