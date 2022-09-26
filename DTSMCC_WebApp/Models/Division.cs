using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTSMCC_WebApp.Models
{

    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
