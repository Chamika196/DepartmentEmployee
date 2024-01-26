using DepartmentEmployeeManager.Api.Models;

namespace DepartmentEmployeeManager.Api.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }    
        public double Salary { get; set; }
        public int DepartmentId { get; set; }

        
    }
}
