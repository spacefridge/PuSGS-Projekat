using PuSGSProjekat.DTO.OrderDTO;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces
{
    public interface IOrderService
    {
        List<OrderResponseDTO> GetAllOrders(long BuyerId,long SellerId);
        OrderResponseDTO GetOrderById(long id);
        OrderResponseDTO CreateOrder(OrderRequestDTO requestDto, long userId);
    }
}
