using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string InvitationCode { get; set; }
    }
}
