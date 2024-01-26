using DepartmentEmployeeManager.Api.Data.Repos;
using DepartmentEmployeeManager.Api.DTOs;
using DepartmentEmployeeManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepartmentEmployeeManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentrepository;

        public DepartmentController(IDepartmentRepository departmentrepository)
        {
            this.departmentrepository = departmentrepository;
        }
        /// Creates a new Department.
        //https://localhost:7278/api/Departments
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var department = new Department
                {
                    
                    DepCode = request.DepCode,
                    DepName = request.DepName,
                };

                await departmentrepository.CreateAsync(department);

                // Map Domain model to DTO
                var response = new DepartmentDto
                {
                    Id = department.Id,
                    DepCode = department.DepCode,
                    DepName = department.DepName,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// Retrieves a list of all Departments.
        //https://localhost:7278/api/Departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var departments = await departmentrepository.GetAllAsync();

                // Return an empty list if no tasks are found
                if (departments == null || !departments.Any())
                {
                    return Ok(new List<DepartmentDto>());
                }

                // Map Domain model to DTO
                var response = new List<DepartmentDto>();
                foreach (var department in departments)
                {
                    response.Add(new DepartmentDto
                    {
                       Id= department.Id,
                       DepCode= department.DepCode,
                       DepName= department.DepName,
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
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            try
            {
                var existingDepartment = await departmentrepository.GetById(id);
                if (existingDepartment == null)
                {
                    return NotFound();
                }

                // Map Domain model to DTO
                var response = new DepartmentDto
                {
                    Id = existingDepartment.Id,
                    DepCode= existingDepartment.DepCode,
                    DepName= existingDepartment.DepName,

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
        public async Task<IActionResult> EditDepartment([FromRoute] int id, UpdateDepartmentRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var department = new Department
                {
                    Id = id,
                    DepCode= request.DepCode,
                    DepName= request.DepName,
                };

                department = await departmentrepository.UpdateAsync(department);

                if (department == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new DepartmentDto
                {
                    Id = department.Id,
                    DepCode= department.DepCode,
                    DepName= department.DepName,
                    
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
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            try
            {
                var department = await departmentrepository.DeleteAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new DepartmentDto
                {
                    Id = department.Id,
                    DepCode= department.DepCode,
                    DepName= department.DepName,
                    
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
