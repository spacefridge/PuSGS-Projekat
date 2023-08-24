using PuSGSProjekat.DTO.DeleteDTO;
using PuSGSProjekat.DTO.OrderDTO;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces
{
    public interface IOrderService
    {
        List<OrderResponseDTO> GetAllOrders(long BuyerId,long SellerId);
        OrderResponseDTO GetOrderById(long id);
        DeleteResponseDTO CancelOrder(long id, long userId);
        OrderResponseDTO CreateOrder(OrderRequestDTO requestDto, long userId);
    }
}
