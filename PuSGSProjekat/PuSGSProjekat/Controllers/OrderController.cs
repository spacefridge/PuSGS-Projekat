using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuSGSProjekat.DTO.OrderDTO;
using PuSGSProjekat.Interfaces;
using System;
using System.Linq;

namespace PuSGSProjekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders([FromQuery] long BuyerId, long SellerId) 
        {
            return Ok(_orderService.GetAllOrders(BuyerId,SellerId));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            OrderResponseDTO order;

            try
            {
                order = _orderService.GetOrderById(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public IActionResult CreateOrder([FromBody] OrderRequestDTO requestDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            OrderResponseDTO order;

            try
            {
                order = _orderService.CreateOrder(requestDto, userId);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(order);
        }
    }
}
