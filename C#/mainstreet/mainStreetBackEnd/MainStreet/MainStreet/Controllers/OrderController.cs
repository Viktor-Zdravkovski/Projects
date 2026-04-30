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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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
        public async Task<IActionResult> GetOrdersByDay(DateTime day)
        {
            //DateTime today = DateTime.UtcNow.Date;
            //DateTime today = DateTime.Today;
            DateTime today = day.Date;


            var orders = await _orderService.GetAllOrdersForCertainDay(today);


        }

    }
}
