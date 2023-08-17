using PuSGSProjekat.Enumerations;
using System;

namespace PuSGSProjekat.DTO.OrderDTO
{
    public class OrderResponseDTO
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DeliveryTime { get; set; }
        public double Price { get; set; }
    }
}
