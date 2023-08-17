using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.OrderDTO;
using PuSGSProjekat.Interfaces;
using System.Collections.Generic;

namespace PuSGSProjekat.Services
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _dbContext;

        public OrderService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderResponseDTO CreateOrder(OrderRequestDTO requestDto, long userId)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderResponseDTO> GetAllOrders(long BuyerId, long SellerId)
        {
            throw new System.NotImplementedException();
        }

        public OrderResponseDTO GetOrderById(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
