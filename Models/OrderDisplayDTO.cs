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
        public int Id { get; set; }

        [Required]
        public XidmetEnum Xidmet { get; set; }

        [Required]
        public CurrencyEnum Vahid { get; set; }

        [Required]
        public int Miqdar { get; set; }

        [Required]
        public decimal Qiymet { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        public int VaqonId { get; set; }

        [Required]
        public decimal TotalQiymet { get; set; }
    }
}
