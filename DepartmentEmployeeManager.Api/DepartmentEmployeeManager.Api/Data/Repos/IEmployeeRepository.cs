using DepartmentEmployeeManager.Api.Models;
using System.Threading.Tasks;

namespace DepartmentEmployeeManager.Api.Data.Repos
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetById(int id);
        Task<Employee?> UpdateAsync(Employee employee);
        Task<Employee?> DeleteAsync(int id);
    }
}
