using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Context
    {
        public int Id { get; set; }
        public User CurrentUser { get; set; }
    }
}
