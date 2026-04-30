using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;
using MainStreet.Dto.OrderItemDto;
using System.ComponentModel.DataAnnotations;

namespace MainStreet.Dto.OrderDto
{
    public class AddOrderDto
    {
        [Required]
        public int UserId { get; set; }

        public ICollection<AddOrderItemDto> OrderItems { get; set; } = new List<AddOrderItemDto>();

    }
}
