using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public DateTime Removed { get; set; }
    }
}
