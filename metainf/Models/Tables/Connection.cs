using System;

namespace metainf.Models
{
    public class Connection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Configuration Configuration { get; set; }
    }
}