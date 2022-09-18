using System;

namespace WebApi.Models
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string PharmacyName { get; set; }
    }
}
