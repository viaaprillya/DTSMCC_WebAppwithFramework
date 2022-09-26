using DTSMCC_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DTSMCC_WebApp.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext): base(dbContext) 
        {
            
        }

        //Atur connection string
        //Memasukkan model yang digunakan untuk melakukan operasi CRUD/Migrasi

        public DbSet<Department> Departments { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}
