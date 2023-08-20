namespace ITPLibrary.Data.Shared.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public decimal? Price { get; set; }
    public DateTime TimeWhenAdded { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public bool Promoted { get; set; }
    public bool Best { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;

}
