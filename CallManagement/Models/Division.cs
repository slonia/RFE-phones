using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace CallManagement.Models
{
    public class Division
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DivisionId { get; set; }

        [ForeignKey("Parent")]
        [Display(Name = "Родительское подразделение")]
        public int? ParentId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        public Division Parent { get; set; }
    }
}