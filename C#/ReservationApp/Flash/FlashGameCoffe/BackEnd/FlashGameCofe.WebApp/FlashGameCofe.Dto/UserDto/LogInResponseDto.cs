using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashGameCofe.Dto.UserDto
{
    public class LogInResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }
    }
}
