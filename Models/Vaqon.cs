using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Models
{
    public class Vaqon
    {
        [Key, Required]
        public int Id { get; set; }

        #nullable enable
        public string? FilePath { get; set; }

        public DocTypeEnum? DocType { get; set; }

        public TypeEnum? Type { get; set; }

        public int? OrderId { get; set; }
    }
}
