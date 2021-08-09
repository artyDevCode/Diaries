using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diaries.Models
{
    public class Log
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        
        [StringLength(255)]
        public string Thread { get; set; }
        [StringLength(50)]
        public string Level { get; set; }
        [StringLength(255)]
        public string Logger { get; set; }
        [StringLength(4000)]
        public string Message { get; set; }
        [StringLength(5000)]
        public string Exception { get; set; }
    }

}