namespace ITPLibrary.Data.Shared.Dtos.BookManagement;

public class BookSellerDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Author { get; set; }
    public string Thumbnail { get; set; }
    public bool Best { get; set; }
}
