﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class VaqonDTOBothBroker: VaqonDTO
    {
        [Required]
        public string Dokument { get; set; }

        [Required]
        public string DokumentNovu { get; set; }

        [Required]
        public string Nov { get; set; }
    }
}
