using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class VaqonDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
