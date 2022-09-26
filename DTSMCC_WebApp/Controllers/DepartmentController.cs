using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTSMCC_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Data;
using DTSMCC_WebApp.Context;
using Microsoft.EntityFrameworkCore;

namespace DTSMCC_WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        //READ
        public IActionResult Index()
        {
            var dept = myContext.Departments.Include(x=>x.Division).ToList();
            return View(dept);
        }

        //CREATE
        public IActionResult Create()
        {
            var div = myContext.Divisions.ToList();
            ViewBag.divisions = ToSelectList(div, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public SelectList ToSelectList(List<Division> divisions, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var div in divisions)
            {
                list.Add(new SelectListItem()
                {
                    Text = div.Name.ToString(),
                    Value = div.Id.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid) 
            {
                myContext.Departments.Add(dept);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var div = myContext.Divisions.ToList();
            ViewBag.divisions = ToSelectList(div, "Id", "Name");

            var dept = myContext.Departments.Where(d => d.Id == id).First();
            return View(dept);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(Department dept)
        {
            if (ModelState.IsValid)
            {
                myContext.Departments.Update(dept);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        public IActionResult Delete(int id)
        {
            var dept = myContext.Departments.Where(d => d.Id == id).First();
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Department dept)
        {
            myContext.Departments.Remove((Department)dept);
            myContext.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
