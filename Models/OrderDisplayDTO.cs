using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class OrderDisplayDTO
    {
        [Key, Required]
        public int Sifaris { get; set; }

        [Required]
        public string Xidmet { get; set; }

        [Required]
        public string Vahid { get; set; }

        [Required]
        public int Miqdar { get; set; }

        [Required]
        public decimal Qiymet { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Tarix { get; set; }

        [Required]
        public int Vaqon { get; set; }

        [Required]
        public decimal TotalQiymet { get; set; }
    }
}
