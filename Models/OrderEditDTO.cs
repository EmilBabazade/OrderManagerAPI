using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class OrderEditDTO
    {
        [Required]
        public string Xidmet { get; set; }

        [Required]
        public string Vahid { get; set; }

        [Required]
        public int Miqdar { get; set; }

        [Required]
        public decimal Qiymet { get; set; }

        [Required]
        public String Tarix { get; set; }

        [Required]
        public int VaqonId { get; set; }
    }
}
