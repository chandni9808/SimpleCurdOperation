using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCurdOperation.Models
{
    public class StudentContext : DbContext //step:3
    {

        public StudentContext(DbContextOptions<StudentContext>options):base(options)
        {

        }
      
        public DbSet<Student> tbl_Student { get; set; } //Database table name
        public DbSet<Departments> tbl_Departments { get; set; } //Database table name
    }
}
