using PuSGSProjekat.Models;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces.Repositories
{
    public interface IOrderRepositories
    {
         void CreateOrder(Order order);
         List<Order> GetAllOrders(long BuyerID,long SellerID);
         Order GetOrderById(long OrderID);
         void CancelOrder(Order order);
         void SaveChanges();
    }
}
