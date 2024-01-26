using DepartmentEmployeeManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeManager.Api.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Employee> Employees { get;set; }
    }
}
