using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsInt.Model.Generic;

namespace WhatsInt.Model
{
    public class UserDto : InteractDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
