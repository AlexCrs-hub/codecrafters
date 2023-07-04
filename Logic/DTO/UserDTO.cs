using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DTO
{
    public class UserDTO
    {   
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
