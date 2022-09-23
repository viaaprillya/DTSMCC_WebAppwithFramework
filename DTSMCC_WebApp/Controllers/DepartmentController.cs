using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTSMCC_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace DTSMCC_WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        SqlConnection connection;
        string connectionString = "Data Source=TARDIS;Initial Catalog=DTSMCC01;User ID=viaaprillya;Password=1234567";

        //READ
        public IActionResult Index()
        {
            string query = "SELECT Department.Id, Department.Name, Divisions.Id, Divisions.Name FROM Department JOIN Divisions ON Department.Division_Id=Divisions.Id";
            connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            List<Department> Departments = new List<Department>();

            try 
            {
                connection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Department department = new Department();
                            Division division = new Division();
                            department.Id = (int)(reader[0]);
                            department.Name = reader[1].ToString();
                            division.Id = (int)(reader[2]);
                            division.Name = reader[3].ToString();
                            department.Div = division;
                            Departments.Add(department);

                        }
                    }
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View(Departments);
        }

        //CREATE
        public IActionResult Create()
        {
            string query = "SELECT * FROM Divisions";
            connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            CreateDepartment createDepartment = new CreateDepartment();
            createDepartment.Department = new Department();
            List<Division> Divisions = new List<Division>();
            try
            {
                connection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Division division = new Division();
                            division.Id = (int)(reader[0]);
                            division.Name = reader[1].ToString();
                            Divisions.Add(division);

                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            List<SelectListItem> divisions = Divisions.OrderBy(n => n.Id).Select(n => new SelectListItem{Value = n.Id.ToString(), Text = n.Name}).ToList();
            createDepartment.Divisions = divisions;

            return View(createDepartment);
        }

        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        int Id = collection["Id"];
        //    }
        //}

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete(int )
        {
            return View();
        }

        //UPDATE

        //DELETE
    }
}
