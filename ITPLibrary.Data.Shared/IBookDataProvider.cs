namespace ITPLibrary.Data.Shared;

public interface IBookDataProvider
{
    Task<List<Book>> GetAll();

    Task<List<Book>> GetPromotedBooks();

    Task<List<Book>> GetBestBooksRecentlyAdded();

    Task<Book> GetById(int id);

    Task<Book> GetBookDetails(int id);

    Task<Book> Add(Book newBookDto);

    Task<Book> Update(Book updatedBook);

    Task<Book> Delete(int id);
}