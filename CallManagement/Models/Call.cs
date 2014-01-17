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
    public class Call
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CallId { get; set; }
                
        [Display(Name = "Номер звонящего")]
        public int FromId { get; set; }

        
        [Display(Name = "Номер отвечающего")]
        public int ToId { get; set; }

        [Display(Name = "Начало звонка")]
        public DateTime Start { get; set; }

        [Display(Name = "Конец звонка")]
        public DateTime End { get; set; }

        [ForeignKey("FromId")]
        public Phone From { get; set; }
        [ForeignKey("ToId")]
        public Phone To { get; set; }

        public double Length
        {
            get 
            {
                return (End - Start).TotalSeconds;
            }
        }

        public string FormattedLength
        {
            get
            {
                return Length + " секунд";
            }
        }

        public Call()
        {
        }

        public Call(Phone PhoneFrom, Phone PhoneTo, DateTime start, DateTime end)
        {
            this.FromId = PhoneFrom.PhoneId;
            this.ToId = PhoneTo.PhoneId;
            this.From = PhoneFrom;
            this.To = PhoneTo;
            this.Start = start;
            this.End = end;
        }


        public double GetCostFor(Phone phone)
        {
            double cost = 0;
            double length = Length;
            if (!phone.Tarif.SecondTarification)
            {
                length = length / 60;
                length = Math.Ceiling(length);
            }
            if (phone == From)
            {
                if (phone.Tarif.Operator == To.Tarif.Operator)
                {
                    cost = length * phone.Tarif.OutCostInNet;
                }
                else
                {
                    cost = length * phone.Tarif.OutCostOutNet;
                }
            }
            else
            {
                if (phone.Tarif.Operator == From.Tarif.Operator)
                {
                    cost = length * phone.Tarif.IncCostInNet;
                }
                else
                {
                    cost = length * phone.Tarif.IncCostOutNet;
                }

            }
            return cost;
        }
    }
}