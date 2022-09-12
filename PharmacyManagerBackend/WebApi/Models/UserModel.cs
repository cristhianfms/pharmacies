using System;

namespace WebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
       // public DateTime Registration { get; set; }
    }
}
