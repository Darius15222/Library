namespace ITPLibrary.Data.Shared.Dtos.ShoppingCartItemDto
{
    public class ShoppingCartItemDto
    {
        int Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int ShoppingCartItemId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public Status Status { get; set; }
    }
}
