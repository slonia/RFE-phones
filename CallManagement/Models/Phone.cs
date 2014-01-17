using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace CallManagement.Models
{
    public class Phone
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PhoneId { get; set; }

        [ForeignKey("Division")]
        [Display(Name = "Подразделение")]
        public int? DivisionId { get; set; }

        [ForeignKey("User")]
        [Display(Name = "Сотрудник")]
        public int? UserId { get; set; }

        [ForeignKey("Tarif")]
        [Display(Name = "Тариф")]
        public int TarifId { get; set; }

        [Display(Name = "Номер")]
        public string Number { get; set; }
        public string ParsedNumber { get; set; }
        public Division Division { get; set; }
        public User User { get; set; }
        public Tarif Tarif { get; set; }
    }

    public class PhoneDBContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}