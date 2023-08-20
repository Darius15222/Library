using ITPLibrary.Data.Shared.Dtos.BookManagement;

namespace ITPLibrary.Api.Core.Services.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooks();
    Task<List<BookSellerDto>> GetBestBooksRecentlyAdded();
    Task<List<PromotedBooksDto>> GetPromotedBooks();
    Task<BookDto> GetBookById(int id);
    Task<BookDetailsDto> GetBookDetails(int id);
    Task<BookDto> AddBook(BookDto newBook);
    Task<BookDto> UpdateBook(BookDto updatedBook);
    Task<BookDto> DeleteBook(int id);
}
