using PuSGSProjekat.Context;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System.Collections.Generic;
using System.Linq;

namespace PuSGSProjekat.Repositories
{
    public class OrderRepository : IOrderRepositories
    {

        private readonly DatabaseContext _dbContext;

        public OrderRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CancelOrder(Order order)
        {
             _dbContext.Orders.Remove(order);
        }

        public void CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);
        }

        public List<Order> GetAllOrders(long BuyerID, long SellerID)
        {

            List<Order> orders = new List<Order>();
            if (BuyerID > 0)
            {
                orders = _dbContext.Orders.Where(x => x.BuyerId == BuyerID).ToList();
            }
            else if (SellerID > 0)
            {
                orders = _dbContext.Orders.Where(x => x.Article.SellerId == SellerID).ToList();
            }
            else
            {
                orders = _dbContext.Orders.ToList();
            }

            return orders;
        }

        public Order GetOrderById(long OrderID)
        {
            return _dbContext.Orders.Find(OrderID);
        }

        public void SaveChanges() 
        {
            _dbContext.SaveChanges();
        }
    }
}
