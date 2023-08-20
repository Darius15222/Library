namespace ITPLibrary.Data.Shared.Dtos.BookManagement;

public class BookDetailsDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}
