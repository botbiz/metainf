using System;
using System.Collections.Generic;

namespace metainf.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        public ICollection<Connection> Connections { get; set; }
    }
}