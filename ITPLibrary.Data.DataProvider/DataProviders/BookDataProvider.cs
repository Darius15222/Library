namespace ITPLibrary.Data.DataProvider.DataProviders
{
    public class BookDataProvider : IBookDataProvider
    {
        private readonly SqlConnector _sqlConnector;

        public BookDataProvider(IConfiguration configuration)
        {
            _sqlConnector = new SqlConnector(configuration[Constants.DatabaseConnectionString]);
        }

        public async Task<List<Book>> GetAll()
        {
            var sqlQuery = "SELECT * FROM Books";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryAsync<Book>(sqlQuery);
                
                return result.ToList();
            }
        }

        public async Task<List<Book>> GetPromotedBooks()
        {
            var sqlQuery = "SELECT Title, Description as ShortDescription, ShortDescription = CONCAT( SUBSTRING(Description, 1, 20) , '...'), Image FROM Books WHERE Promoted = 'true'";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryAsync<Book>(sqlQuery);
                return result.ToList();
            }
        }

        public async Task<List<Book>> GetBestBooksRecentlyAdded()
        {
            var sqlQuery = "SELECT * FROM Books WHERE Best = 'true' AND ABS( DATEDIFF(hour, TimeWhenAdded, GETDATE())) < 12 ";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryAsync<Book>(sqlQuery);
                return result.ToList();
            }
        }

        public async Task<Book> GetById(int id)
        {
            var sqlQuery = "SELECT * FROM Books WHERE id = @id";
            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Book>(sqlQuery, new { id });
                return result;
            }
        }

        public async Task<Book> GetBookDetails(int id)
        {
            var sqlQuery = "SELECT Id, Title, Price, Author, Description as longDescription, Image FROM Books WHERE id = @id";
            using (var connection = _sqlConnector.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Book>(sqlQuery, new { id });
            }
        }

        public async Task<Book> Add(Book newBookDto)
        {
            var sqlQuery = "INSERT INTO Books (Title, Author, Price, Description, Promoted, Best, Image, Thumbnail, TimeWhenAdded) " +
                "VALUES (@Title, @Author, @Price, @Description, @Promoted, @Best, @Image, @Thumbnail, @TimeWhenAdded)";

            using (var connection = _sqlConnector.CreateConnection())
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    newBookDto.Title,
                    newBookDto.Author,
                    newBookDto.Description,
                    newBookDto.Promoted,
                    newBookDto.Best,
                    newBookDto.Price,
                    newBookDto.Image,
                    newBookDto.Thumbnail,
                    newBookDto.TimeWhenAdded
                });
            }
            return newBookDto;
        }

        public async Task<Book> Update(Book updatedBook)
        {
            var sqlQuery = "UPDATE Books SET Title = @Title, Author = @Author, Price = @Price, Description = @Description, " +
                "Promoted = @Promoted, Image = @Image, Thumbnail = @Thumbnail, TimeWhenAdded = @TimeWhenAdded WHERE id = @Id";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sqlQuery, updatedBook);
                if (result == 0)
                    return null;
                return updatedBook;
            }
        }

        public async Task<Book> Delete(int id)
        {
            var selectQuery = "SELECT * FROM Books WHERE id = @id";
            var deleteQuery = "DELETE FROM Books WHERE id = @id";

            using (var connection = _sqlConnector.CreateConnection())
            {
                var bookToDelete = await connection.QuerySingleOrDefaultAsync<Book>(selectQuery, new { id });

                if (bookToDelete == null)
                    return null;
                
                await connection.ExecuteAsync(deleteQuery, new { id });

                return bookToDelete;
            }
        }

    }
}
