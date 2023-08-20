using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Data.Shared.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public Guid ShoppingCartId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
