using System;
using System.Collections.Generic;
using System.Text;
using Exceptions;

namespace Domain
{
    public class Role
    {
        private string name;
        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ValidationException("Role name can't be empty");
                }
                name = value;
            }
        }
    }
}
