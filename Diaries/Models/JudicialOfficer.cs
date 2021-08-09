using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class JudicialOfficer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int JO_ID { get; set; }
        [StringLength(100)]
        [Display(Name = "Judicial Officer"), Required]
        public string JO_Name { get; set; }
        [StringLength(10)]
        [Display(Name = "Bacground Colour"), Required]
        public string JO_BackColor { get; set; }
        [StringLength(10)]
        [Display(Name = "Text Colour"), Required]
        public string JO_TextColor { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public virtual ICollection<JudicialOfficerListing> VP_JOListings { get; set; }

    }
}