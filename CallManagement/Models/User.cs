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
    public class User
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Глава своего подразделения")]
        public bool HeadOfDivision { get; set; }
        [Display(Name = "Мужской пол")]
        public bool Man { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }
        public Division Division { get; set; }

        public string FullName 
        {
            get
            {
              return LastName + " " + FirstName;
            }
        }
    }
}