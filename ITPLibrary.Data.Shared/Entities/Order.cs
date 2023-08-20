using ITPLibrary.Data.Shared.Dtos.OrderDto;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Data.Shared.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public string Email { get; set; } = string.Empty;

        public string Billing_Country { get; set; } = string.Empty;
        public string Billing_Address { get; set; } = string.Empty ;
        public string Billing_Number { get; set; } = string.Empty;

        public string Delivery_Country { get; set; } = string.Empty;
        public string Delivery_Address { get; set; } = string.Empty;
        public string Delivery_Number { get; set; } = string.Empty;

        public PaymentType PaymentType { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderPlaced { get; set; }
        public DateTime DeliveryDate { get; set; }


        public string Observations { get; set; } = string.Empty;
    }
}
