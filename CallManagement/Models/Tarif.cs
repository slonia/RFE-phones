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
    public class Tarif
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TarifId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Оператор")]
        public string Operator { get; set; }
        [Display(Name = "Стоимость входящего (внутри сети)")]
        public double IncCostInNet { get; set; }
        [Display(Name = "Стоимость исходящего (внутри сети)")]
        public double OutCostInNet { get; set; }
        [Display(Name = "Стоимость входящего (вне сети)")]
        public double IncCostOutNet { get; set; }
        [Display(Name = "Стоимость исходящего (вне сети)")]
        public double OutCostOutNet { get; set; }
        [Display(Name = "Абонентская плата")]
        public double MonthlyCost { get; set; }
        [Display(Name = "Посекундная тарификация")]
        public bool SecondTarification { get; set; }

        public Tarif()
        {
            MonthlyCost = 0.0;
            SecondTarification = false;
        }

        public string FullName
        {
            get
            {
             return Operator + " - " + Name;
            }
            
        }
    }
}