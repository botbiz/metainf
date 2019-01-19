using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metainf.Models
{
    [Table("Connection")]
    public class Connection
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Type { get; set; }
        [MaxLength(200)]
        public string Host { get; set; }
        [MaxLength(200)]
        public string Port { get; set; }
        [MaxLength(200)]
        public string Database { get; set; }
        [MaxLength(200)]
        public string Login { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }

        //[NotMapped]
        //public ICollection<FromTo> FromToList { get; set; }
    }
}