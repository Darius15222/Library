namespace ITPLibrary.Data.Shared.Dtos.BookManagement;

public class PromotedBooksDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}
