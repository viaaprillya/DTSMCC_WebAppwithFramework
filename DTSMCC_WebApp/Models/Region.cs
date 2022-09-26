using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTSMCC_WebApp.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Division Division { get; set; }
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
    }

}
