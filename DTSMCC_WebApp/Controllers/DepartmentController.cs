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
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
            catch (Exception ex)
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
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ViewBag.divisions = ToSelectList(dt, "Id", "Name");
            return View();
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter deptID = new SqlParameter();
                deptID.ParameterName = "@deptID";
                deptID.Value = dept.Id;

                SqlParameter deptName = new SqlParameter();
                deptName.ParameterName = "@deptName";
                deptName.Value = dept.Name;

                SqlParameter div = new SqlParameter();
                div.ParameterName = "@div";
                div.Value = dept.Div.Id;

                sqlCommand.Parameters.Add(deptID);
                sqlCommand.Parameters.Add(deptName);
                sqlCommand.Parameters.Add(div);
                try
                {
                    sqlCommand.CommandText = "INSERT INTO Department " +
                        "(Id, Name, Division_Id) VALUES (@deptID, @deptName, @div)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            string query = "SELECT * FROM Divisions";
            connection = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ViewBag.divisions = ToSelectList(dt, "Id", "Name");


            Department dept = new Department();
            dept.Id = id;

            Division div = new Division();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.CommandText = "SELECT * FROM Department WHERE Id = @deptID";
                sqlCommand.Parameters.Add("@deptID", SqlDbType.Int).Value = id;

                using (SqlDataReader read = sqlCommand.ExecuteReader())
                {
                    while (read.Read())
                    {
                        dept.Id = (int)read[0];
                        dept.Name = read[1].ToString();
                        div.Id = (int)read[2];
                    }
                    dept.Div = div;
                }
            }
            return View(dept);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirmed(Department dept)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter deptID = new SqlParameter();
                deptID.ParameterName = "@deptID";
                deptID.Value = dept.Id;

                SqlParameter deptName = new SqlParameter();
                deptName.ParameterName = "@deptName";
                deptName.Value = dept.Name;

                SqlParameter div = new SqlParameter();
                div.ParameterName = "@div";
                div.Value = dept.Div.Id;

                sqlCommand.Parameters.Add(deptID);
                sqlCommand.Parameters.Add(deptName);
                sqlCommand.Parameters.Add(div);
                try
                {
                    sqlCommand.CommandText = "UPDATE Department " +
                        "SET Name=@deptName, Division_Id=@div WHERE Id=@deptID";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return RedirectToAction("Index");
        }
    

        public IActionResult Delete(int id)
        {
            Department dept = new Department();
            dept.Id = id;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@id";
                Id.Value = id;

                sqlCommand.Parameters.Add(Id);


                try
                {
                    sqlCommand.CommandText = "SELECT Name FROM Department WHERE Id = @id ";
                    dept.Name = sqlCommand.ExecuteScalar().ToString();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Console.WriteLine("print id :"+id);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@id";
                Id.Value = id;

                sqlCommand.Parameters.Add(Id);


                try
                {
                    sqlCommand.CommandText = "DELETE FROM Department WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
