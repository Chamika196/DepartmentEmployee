using DepartmentEmployeeManager.Api.Models;
using System.Threading.Tasks;

namespace DepartmentEmployeeManager.Api.Data.Repos
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetById(int id);
        Task<Department?> UpdateAsync(Department department);
        Task<Department?> DeleteAsync(int id);
    }
}
