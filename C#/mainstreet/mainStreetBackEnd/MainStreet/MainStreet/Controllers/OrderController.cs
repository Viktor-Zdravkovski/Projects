using MainStreet.Dto.OrderDto;
using MainStreet.Dto.PaymentDto;
using MainStreet.Services.Interfaces;
using MainStreet.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainStreet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrderById/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Order with ID:{id} was not found");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrderByUserId/{userId}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetOrderByUser(int userId, int count)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == "Admin" || x.Type == "Staff")?.Value;
                if (role == null || role != "Admin" || role != "Staff")
                {
                    return Forbid();
                }

                var orders = await _orderService.GetOrderByUserId(userId, count);
                return Ok(orders);

            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Order with user ID:{userId} was not found");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrdersByDay")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetOrdersByACertainDay([FromQuery] DateTime? day)
        {
            try
            {
                var searchDate = day ?? DateTime.UtcNow.Date;

                var orders = await _orderService.GetAllOrdersForCertainDay(searchDate);

                return Ok(orders);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"No orders for that date: {day}");
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDto addOrderDto)
        {
            try
            {
                if (addOrderDto == null)
                {
                    return BadRequest("Reservation data is required");
                }

                await _orderService.AddOrder(addOrderDto);
                return Ok(new { message = "Order added successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] int id, UpdateOrderDto updateOrderDto)
        {
            if (updateOrderDto == null)
                return BadRequest("Invalid reservation data.");

            try
            {
                await _orderService.UpdateOrder(id, updateOrderDto);
                return Ok("Reservation updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrder(id);
                return Ok($"The order with ID: {id} was successfully deleted");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Order with ID:{id} was not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

//Task DeleteOrder(int id);