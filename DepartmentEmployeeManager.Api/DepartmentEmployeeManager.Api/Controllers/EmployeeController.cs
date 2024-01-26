using DepartmentEmployeeManager.Api.Data.Repos;
using DepartmentEmployeeManager.Api.DTOs;
using DepartmentEmployeeManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeEmployeeManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        /// Creates a new Employee.
        //https://localhost:7278/api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var employee = new Employee
                {

                    FirstName= request.FirstName,
                    LastName= request.LastName,
                    Email= request.Email,
                    DOB= request.DOB, //Parse DOB string to DateTime
                    Salary= request.Salary,
                    DepartmentId= request.DepartmentId,

                    
                };
                Console.WriteLine(employee.Age+"dfhgjhjkkkkkkkkkkkkkkkkkkkkkkkkkk");
                Debug.WriteLine(employee.Age+"1111111111111111111111111111111111111111111111111");
                await employeeRepository.CreateAsync(employee);

                // Map Domain model to DTO
                var response = new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email= employee.Email,
                    DOB= employee.DOB,
                    Salary= employee.Salary,
                    DepartmentId= employee.DepartmentId,
                    Age= employee.Age
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// Retrieves a list of all Employees.
        //https://localhost:7278/api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var employees = await employeeRepository.GetAllAsync();

                // Return an empty list if no tasks are found
                if (employees == null || !employees.Any())
                {
                    return Ok(new List<EmployeeDto>());
                }

                // Map Domain model to DTO
                var response = new List<EmployeeDto>();
                foreach (var employee in employees)
                {
                    response.Add(new EmployeeDto
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName= employee.LastName,
                        Email= employee.Email,
                        DOB= employee.DOB,
                        Salary= employee.Salary,
                        DepartmentId= employee.DepartmentId,
                        //age= employee.Age,
                       
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// Retrieves a TaskM by its ID.
        //https://localhost:7278/api/TaskMs/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var existingEmployee = await employeeRepository.GetById(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Map Domain model to DTO
                var response = new EmployeeDto
                {
                    Id = existingEmployee.Id,
                    FirstName = existingEmployee.FirstName,
                    LastName = existingEmployee.LastName,
                    Email = existingEmployee.Email,
                    DOB = existingEmployee.DOB,
                    Salary = existingEmployee.Salary,
                    DepartmentId = existingEmployee.DepartmentId,
                    //age = existingEmployee.Age,

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// Updates an existing TaskM by its ID.
        //https://localhost:7278/api/TaskMs/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditEmployee([FromRoute] int id, UpdateEmployeeRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var employee = new Employee
                {
                    Id = id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    DOB = request.DOB,
                    Salary = request.Salary,
                    DepartmentId = request.DepartmentId,
                    
                };

                employee = await employeeRepository.UpdateAsync(employee);

                if (employee == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new EmployeeDto
                {
                    Id = id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    DOB = employee.DOB,
                    Salary = employee.Salary,
                    DepartmentId = employee.DepartmentId,
                    

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// Deletes a TaskM by its ID.
        //https://localhost:7278/api/TaskMs/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                var employee = await employeeRepository.DeleteAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    DOB = employee.DOB,
                    Salary = employee.Salary,
                    DepartmentId = employee.DepartmentId,
                    

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
