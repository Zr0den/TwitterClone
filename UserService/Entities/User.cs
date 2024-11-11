﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserTag { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}