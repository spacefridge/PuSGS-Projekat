using AutoMapper;
using EntityFramework.Exceptions.Common;
using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.DeleteDTO;
using PuSGSProjekat.DTO.OrderDTO;
using PuSGSProjekat.Enumerations;
using PuSGSProjekat.ExceptionHandler;
using PuSGSProjekat.Interfaces;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuSGSProjekat.Services
{
    public class OrderService : IOrderService
    {
   
        private readonly IMapper _mapper;
        private readonly IOrderRepositories _orderRepositories;
        private readonly IArticleRepositories _articleRepositories;

        public OrderService(IOrderRepositories orderRepositories, IMapper mapper,IArticleRepositories articleRepositories)
        {
            _orderRepositories = orderRepositories;
            _mapper = mapper;
            _articleRepositories = articleRepositories;
        }

        public OrderResponseDTO CreateOrder(OrderRequestDTO requestDto, long userId)
        {
            Order order = _mapper.Map<Order>(requestDto);
            order.BuyerId = userId;

            Article article = _articleRepositories.GetArticleById(order.ArticleId);

            if (article == null)
            {
                throw new ResourceMissing("Article with specified id doesn't exist!");
            }

            if (article.Quantity < order.Quantity)
            {
                throw new InvalidField("There are not enough articles in stock!");
            }

            article.Quantity -= order.Quantity;
            order.OrderState = OrderState.Pending;
            order.Price = article.Price * order.Quantity;
            order.CreatedAt = DateTime.UtcNow;
            order.DeliveryTime = new Random().Next(1, 50);

            _orderRepositories.CreateOrder(order);

            try
            {
                _orderRepositories.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidField("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<OrderResponseDTO>(order);
        }

        public DeleteResponseDTO CancelOrder(long id, long userId)
        {
            Order order = _orderRepositories.GetOrderById(id);

            if (order == null)
            {
                throw new ResourceMissing("No order with given id.");
            }

            if (order.BuyerId != userId)
            {
                throw new Forbidden("Not your order, cant proceed.");
            }

            if ((DateTime.UtcNow - order.CreatedAt).Hours > 1)
            {
                throw new InvalidField("Cancelation time expired");
            }

            Article article = _articleRepositories.GetArticleById(order.ArticleId);

            if (article == null)
            {
                throw new ResourceMissing("No article with given id.");
            }

            article.Quantity += order.Quantity;

            _orderRepositories.CancelOrder(order);
            _orderRepositories.SaveChanges();

            return _mapper.Map<DeleteResponseDTO>(order);
        }

        public List<OrderResponseDTO> GetAllOrders(long BuyerId, long SellerId)
        {
            List<Order> orders = new List<Order>();

            orders = _orderRepositories.GetAllOrders(BuyerId, SellerId);

            return _mapper.Map<List<OrderResponseDTO>>(orders);
        }

        public OrderResponseDTO GetOrderById(long id)
        {
            OrderResponseDTO order = _mapper.Map<OrderResponseDTO>(_orderRepositories.GetOrderById(id));

            if (order == null)
            {
                throw new ResourceMissing("No order with given id.");
            }
            return order;
        }

        
    }
}
