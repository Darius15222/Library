namespace ITPLibrary.Data.Repositories;

public class BookRepository : IBookDataProvider
{
    private readonly AppDbContext _appDbContext;
    public BookRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Book>> GetAll()
    {
        var books = await _appDbContext.Books.ToListAsync();
        if (books is null)
            return null;

        return books;
    }

    public async Task<List<Book>> GetPromotedBooks()
    {
        var books = await _appDbContext.Books.Where(c => c.Promoted).ToListAsync();

        if (books is null)
            return null;

        foreach (var book in books)
        {
            if (!string.IsNullOrEmpty(book.Description) && book.Description.Length > 20)
            {
                book.Description = string.Concat(book.Description.Substring(0, 20), "...");
            }
        }

        return books;
    }

    public async Task<List<Book>> GetBestBooksRecentlyAdded()
    {
        var expire = DateTime.Now.AddHours(-96);
        var books = await _appDbContext.Books
            .Where(c => c.Best && c.TimeWhenAdded > expire)
            .ToListAsync();

        if (books is null)
            return null;

        return books;
    }

    public async Task<Book> GetById(int id)
    {
        var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        return book;
    }

    public async Task<Book> GetBookDetails(int id)
    {

        var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        return book;
    }

    public async Task<Book> Add(Book book)
    {

        try
        {

            _appDbContext.Books.Add(book);
            await _appDbContext.SaveChangesAsync();

            return book;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Book> Update(Book updatedBook)
    {
        try
        {
            Book book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == updatedBook.Id);

            if (book is null)
                return null;

            _appDbContext.Entry(book).CurrentValues.SetValues(updatedBook);
            await _appDbContext.SaveChangesAsync();

            return book;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Book> Delete(int id)
    {
        try
        {
            Book book = await _appDbContext.Books.FindAsync(id);

            _appDbContext.Books.Remove(book);
            await _appDbContext.SaveChangesAsync();
            return book;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
