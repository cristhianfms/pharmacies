using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public DateTime RegistrationDate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   UserName == user.UserName &&
                   RoleId == user.RoleId &&
                   Email == user.Email &&
                   Address == user.Address &&
                   Password == user.Password &&
                   PharmacyId == user.PharmacyId &&
                   RegistrationDate == user.RegistrationDate;
        }
    }
}
