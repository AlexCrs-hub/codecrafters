using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class GeneratePassword
    {
        public GeneratePassword()
        {
        }

        public string CreatePassword(Guid id, DateTime time) 
        {
            var password = String.Concat(id, time);
            return password;
        }
    }
}
