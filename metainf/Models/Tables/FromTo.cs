using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metainf.Models
{
    [Table("FromTo")]
    public class FromTo
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public bool CanUpdate { get; set; }
        public bool Active { get; set; }

        public Connection ConnectionFrom { get; set; }
        public Connection ConnectionTo { get; set; }

        [MaxLength(200)]
        public string TableFrom { get; set; }
        [MaxLength(200)]
        public string TableTo { get; set; }

        //[NotMapped]
        //public ICollection<Column> ColumnsToList { get; set; }
        //[NotMapped]
        //public ICollection<Column> ColumnsFromList { get; set; }
    }
}