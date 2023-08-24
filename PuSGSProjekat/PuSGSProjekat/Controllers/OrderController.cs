using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuSGSProjekat.DTO.DeleteDTO;
using PuSGSProjekat.DTO.OrderDTO;
using PuSGSProjekat.ExceptionHandler;
using PuSGSProjekat.Interfaces;
using System;
using System.Linq;

namespace PuSGSProjekat.Controllers
{
    [Route("api/orders")]
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
            catch (ResourceMissing e)
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
            catch (ResourceMissing e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidField e)
            {
                return BadRequest(e.Message);
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteOrder(long id)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            DeleteResponseDTO responseDto;

            try
            {
                responseDto = _orderService.CancelOrder(id, userId);
            }
            catch (ResourceMissing e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidField e)
            {
                return BadRequest(e.Message);
            }
            catch (Forbidden)
            {
                return Forbid();
            }

            return Ok(responseDto);
        }
    }
}
