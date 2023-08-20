using ITPLibrary.Data.Shared.Dtos.BookManagement;

namespace ITPLibrary.Api.Core.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookDataProvider _bookDataProvider;
    private readonly IMapper _mapper;

    public BookService(IBookDataProvider bookDataProvider, IMapper mapper)
    {
        _bookDataProvider = bookDataProvider;
        _mapper = mapper;
    }

    public async Task<List<BookDto>> GetAllBooks()
    {
        var book = await _bookDataProvider.GetAll();

        var bookDto = _mapper.Map<List<BookDto>>(book);

        return bookDto;
    }
    public async Task<List<BookSellerDto>> GetBestBooksRecentlyAdded()
    {
        var book = await _bookDataProvider.GetBestBooksRecentlyAdded();
        
        var bookDto = _mapper.Map<List<BookSellerDto>>(book);

        return bookDto;
    }
    public async Task<List<PromotedBooksDto>> GetPromotedBooks()
    {
        var book = await _bookDataProvider.GetPromotedBooks();
        var bookDto = _mapper.Map<List<PromotedBooksDto>>(book);

        return bookDto;
    }
    public async Task<BookDto> GetBookById(int id)
    {
        var book = await _bookDataProvider.GetById(id);
        var bookDto = _mapper.Map<BookDto>(book);

        return bookDto;
    }
    public async Task<BookDetailsDto> GetBookDetails(int id)
    {
        var book = await _bookDataProvider.GetBookDetails(id);

        var bookDto = _mapper.Map<BookDetailsDto>(book);

        return bookDto;
    }
    public async Task<BookDto> AddBook(BookDto newBook)
    {
        var book = _mapper.Map<Book>(newBook);

        var addedBook = await _bookDataProvider.Add(book);

        var bookDto = _mapper.Map<BookDto>(addedBook);

        return bookDto;
    }
    public async Task<BookDto> UpdateBook(BookDto newBook)
    {
        var book = _mapper.Map<Book>(newBook);

        var updatedBook = await _bookDataProvider.Update(book);

        var bookDto = _mapper.Map<BookDto>(updatedBook);

        return bookDto;
    }
    public async Task<BookDto> DeleteBook(int id)
    {
        var book = await _bookDataProvider.Delete(id);

        var bookDto = _mapper.Map<BookDto>(book);

        return bookDto;
    }
}