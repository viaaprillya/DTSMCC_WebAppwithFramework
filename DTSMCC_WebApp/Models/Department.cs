using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTSMCC_WebApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Division Div { get; set; }
    }

    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateDepartment
    {
        public virtual Department Department { get; set; }
        public IEnumerable<SelectListItem> Divisions { get; set; }
    }


}
