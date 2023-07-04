using Microsoft.AspNetCore.Identity;
using System;

namespace Logic
{
    public class User : IdentityUser<Guid>, IEntity
    {   

        public string OneTimePassword { get; set; }
        public bool PasswordExpired { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public DateTime Removed { get; set; }
    }
}
