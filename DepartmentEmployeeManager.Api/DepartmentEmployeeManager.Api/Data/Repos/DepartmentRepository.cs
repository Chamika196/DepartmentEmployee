using DepartmentEmployeeManager.Api.Data.Context;
using DepartmentEmployeeManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DepartmentEmployeeManager.Api.Data.Repos
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Department> CreateAsync(Department department)
        {
            await db.Departments.AddAsync(department);
            await db.SaveChangesAsync();

            return department;
        }

        public async Task<Department?> DeleteAsync(int id)
        {
            var existingDepartment= await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDepartment is null)
            {
                return null;
            }

            db.Departments.Remove(existingDepartment);
            await db.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<Department?> GetById(int id)
        {
            return await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            var existingDepartment = await db.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);

            if (existingDepartment != null)
            {
                db.Entry(existingDepartment).CurrentValues.SetValues(department);
                await db.SaveChangesAsync();
                return department;
            }

            return null;
        }
    }
}
