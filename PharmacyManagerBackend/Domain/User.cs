﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime Registration { get; set; }
    }
}
