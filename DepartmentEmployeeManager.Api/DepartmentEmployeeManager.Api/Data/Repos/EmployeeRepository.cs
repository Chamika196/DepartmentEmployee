using DepartmentEmployeeManager.Api.Data.Context;
using DepartmentEmployeeManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeManager.Api.Data.Repos
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            employee.Age = CalculateAge(employee.DOB);
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();

            return employee;
        }
        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.UtcNow; // Use UTC to avoid timezone issues
            int age = today.Year - dob.Year;
            if (today.Month < dob.Month || (today.Month == dob.Month && today.Day < dob.Day))
            {
                age--;
            }
            return age;
        }

        public async Task<Employee?> DeleteAsync(int id)
        {
            var existingEmployee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existingEmployee is null)
            {
                return null;
            }

            db.Employees.Remove(existingEmployee);
            await db.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await db.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            var existingEmployee = await db.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (existingEmployee != null)
            {
                db.Entry(existingEmployee).CurrentValues.SetValues(employee);
                await db.SaveChangesAsync();
                return employee;
            }

            return null;
        }
    }
}
