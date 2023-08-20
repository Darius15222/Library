using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Data.Shared.Entities
{
    public class ShoppingCartItem
    {
        [Key]
        int Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingCartItemId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
