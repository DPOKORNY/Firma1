using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Firma1.Models
{
    public class Heslo
    {
        private string heslo = "Aa47Loi369";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool ZvalidujHeslo()
        {
            if (Password == heslo)
            {
                return true;
            }

            return false;
        }
    }

    
}