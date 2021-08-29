using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class VaqonBrokerXidmetindenKecmiDTO
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        public TypeEnum Nov { get; set; }
    }
}
