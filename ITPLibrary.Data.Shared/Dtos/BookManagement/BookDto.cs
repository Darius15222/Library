namespace ITPLibrary.Data.Shared.Dtos.BookManagement;

public class BookDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public decimal? Price { get; set; }
    public DateTime TimeWhenAdded { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public bool Promoted { get; set; }
    public bool Best { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
}
