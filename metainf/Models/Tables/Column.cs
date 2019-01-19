using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metainf.Models
{
    [Table("Column")]
    public class Column
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        
        public FromTo FromTo { get; set; }
    }
}