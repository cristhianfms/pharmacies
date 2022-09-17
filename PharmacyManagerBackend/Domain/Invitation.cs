using System;
using System.Collections.Generic;
using System.Text;
using Exceptions;

namespace Domain
{
    public class Invitation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Code { get; set; }

        public void CheckIsValid()
        {
            if (String.IsNullOrEmpty(UserName))
            {
                throw new ValidationException("Name can't be empty");
            }
        }
    }
}
