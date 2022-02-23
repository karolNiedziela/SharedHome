using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.Identity.Models
{
    public class LoginRequest
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
