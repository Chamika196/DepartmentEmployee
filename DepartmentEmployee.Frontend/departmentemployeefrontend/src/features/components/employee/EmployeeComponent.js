import React, { useState, useEffect } from 'react';
import axios from 'axios';

const EmployeeComponent = () => {
    const [employees, setEmployees] = useState([]);
    const [newEmployee, setNewEmployee] = useState({
        firstName: '',
        lastName: '',
        email: '',
        dob: '',
        age: 0,
        salary: 0,
        departmentId: null,
    });
    const [selectedEmployee, setSelectedEmployee] = useState(null);
    const [departments, setDepartments] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [departmentsResponse, employeesResponse] = await Promise.all([
                    axios.get('https://localhost:7239/api/Department'),
                    axios.get('https://localhost:7239/api/Employee')
                ]);

                setDepartments((prevDepartments) => departmentsResponse.data);

                const employeesWithDepartments = employeesResponse.data.map((employee) => ({
                    ...employee,
                    department: employee.departmentId ? departmentsResponse.data.find(dep => dep.id === employee.departmentId) : null,
                }));

                setEmployees(employeesWithDepartments);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);  // empty dependency array


    const fetchEmployees = async () => {
        try {
            const response = await axios.get('https://localhost:7239/api/Employee');
            const employeesWithDepartments = response.data.map((employee) => ({
                ...employee,
                department: employee.departmentId ? departments.find(dep => dep.id === employee.departmentId) : null,
            }));
            setEmployees(employeesWithDepartments);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    console.log(employees);
    const fetchDepartments = async () => {
        try {
            const response = await axios.get('https://localhost:7239/api/Department');
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    const handleInputChange = (e) => {
        const calculateAge = (dob) => {
            const dobDate = new Date(dob);
            const age = new Date().getFullYear() - dobDate.getFullYear();
            return isNaN(age) ? 0 : age;
        };
    
        if (!selectedEmployee) {
            // If there is no selectedEmployee, update newEmployee
            setNewEmployee((prevNewEmployee) => {
                const updatedNewEmployee = {
                    ...prevNewEmployee,
                    [e.target.name]: e.target.value,
                };
    
                if (e.target.name === 'dob') {
                    // Calculate age from date of birth
                    updatedNewEmployee.age = calculateAge(e.target.value);
                }
    
                // Ensure departmentId is not null
                updatedNewEmployee.departmentId = updatedNewEmployee.departmentId || '';
    
                return updatedNewEmployee;
            });
        } else {
            // If there is a selectedEmployee, update selectedEmployee
            setSelectedEmployee((prevSelectedEmployee) => {
                const updatedSelectedEmployee = {
                    ...prevSelectedEmployee,
                    [e.target.name]: e.target.value,
                };
    
                if (e.target.name === 'dob') {
                    // Calculate age from date of birth
                    updatedSelectedEmployee.age = calculateAge(e.target.value);
                }
    
                // Ensure departmentId is not null
                updatedSelectedEmployee.departmentId = updatedSelectedEmployee.departmentId || '';
    
                return updatedSelectedEmployee;
            });
        }
    };
    
    
    

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await axios.post('https://localhost:7239/api/Employee', newEmployee);
            fetchEmployees();
            setNewEmployee({
                firstName: '',
                lastName: '',
                email: '',
                dob: '',
                age: 0,
                salary: 0,
                departmentId: null,
            });
        } catch (error) {
            console.error('Error adding employee:', error);
        }
    };

    const handleEditClick = async (id) => {
        try {
            const response = await axios.get(`https://localhost:7239/api/Employee/${id}`);
            const fetchedEmployee = response.data;

            setSelectedEmployee({
                id: fetchedEmployee.id,
                firstName: fetchedEmployee.firstName,
                lastName: fetchedEmployee.lastName,
                email: fetchedEmployee.email,
                dob: fetchedEmployee.dob,
                age: fetchedEmployee.age,
                salary: fetchedEmployee.salary,
                departmentId: fetchedEmployee.departmentId,
            });
        } catch (error) {
            console.error('Error fetching employee for edit:', error);
        }
    };

    const handleEditSubmit = async () => {
        try {
            // Include the 'age' field in the selectedEmployee object
            const editedEmployee = {
                ...selectedEmployee,
                age: selectedEmployee.age,
            };
    
            await axios.put(`https://localhost:7239/api/Employee/${selectedEmployee.id}`, editedEmployee);
            fetchEmployees();
            setSelectedEmployee(null);
        } catch (error) {
            console.error('Error editing employee:', error);
        }
    };
    

    const handleDeleteClick = async (id) => {
        try {
            await axios.delete(`https://localhost:7239/api/Employee/${id}`);
            fetchEmployees();
        } catch (error) {
            console.error('Error deleting employee:', error);
        }
    };

    return (
        <div className='container'>
            <div className='card mt-3'>
                <div className='card-body'>
                    <div className='row justify-content-center'>
                        <h5 className='card-title text-center'>Employees</h5>
                    </div>
                    <form onSubmit={selectedEmployee ? handleEditSubmit : handleSubmit}>
                        <div className='row'>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>First Name:</label>
                                    <input
                                        type='text'
                                        className='form-control'
                                        name='firstName'
                                        value={selectedEmployee ? selectedEmployee.firstName : newEmployee.firstName}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>Last Name:</label>
                                    <input
                                        type='text'
                                        className='form-control'
                                        name='lastName'
                                        value={selectedEmployee ? selectedEmployee.lastName : newEmployee.lastName}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>Email:</label>
                                    <input
                                        type='email'
                                        className='form-control'
                                        name='email'
                                        value={selectedEmployee ? selectedEmployee.email : newEmployee.email}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                        </div>
                        <div className='row'>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>Date of Birth:</label>
                                    <input
                                        type='date'
                                        className='form-control'
                                        name='dob'
                                        value={selectedEmployee ? selectedEmployee.dob : newEmployee.dob}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>Age:</label>
                                    <input
                                        disabled
                                        type='number'
                                        className='form-control'
                                        name='age'
                                        value={selectedEmployee ? selectedEmployee.age : newEmployee.age}
                                        onChange={handleInputChange}
                                        readOnly // Age is calculated automatically, so it's read-only
                                    />
                                </div>
                            </div>
                            <div className='col-md-4'>
                                <div className='mb-3'>
                                    <label className='form-label'>Salary:</label>
                                    <input
                                        type='number'
                                        className='form-control'
                                        name='salary'
                                        value={selectedEmployee ? selectedEmployee.salary : newEmployee.salary}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                        </div>
                        <div className='row'>
                            <div className='col-md-6'>
                                <div className='mb-3'>
                                    <label className='form-label'>Department:</label>
                                    <select
                                        className='form-select'
                                        name='departmentId'
                                        value={selectedEmployee ? selectedEmployee.departmentId : newEmployee.departmentId}
                                        onChange={handleInputChange}
                                    >
                                        <option value={null}>Select Department</option>
                                        {departments.map((department) => (
                                            <option key={department.id} value={department.id}>
                                                {department.depName}
                                            </option>
                                        ))}
                                    </select>
                                </div>
                            </div>
                        </div>
                        <button type='submit' className='btn btn-success'>
                            {selectedEmployee ? 'Edit Employee' : 'Add Employee'}
                        </button>
                    </form>
                </div>
            </div>

            <table className='table table-dark table-hover mt-4'>
                <thead>
                    <tr>
                        <th scope='col'>Employee ID</th>
                        <th scope='col'>First Name</th>
                        <th scope='col'>Last Name</th>
                        <th scope='col'>Email</th>
                        <th scope='col'>Date of Birth</th>
                        <th scope='col'>Age</th>
                        <th scope='col'>Salary</th>
                        <th scope='col'>Department</th>
                        <th scope='col'>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map((employee) => (
                        <tr key={employee.id}>
                            <td>{employee.id}</td>
                            <td>{employee.firstName}</td>
                            <td>{employee.lastName}</td>
                            <td>{employee.email}</td>
                            <td>{employee.dob}</td>
                            <td>{employee.age}</td>
                            <td>{employee.salary}</td>
                            <td>{employee.department && employee.department.depName ? employee.department.depName : ''}</td>

                            <td>
                                <button className='btn btn-warning' onClick={() => handleEditClick(employee.id)}>
                                    Edit
                                </button>
                                &nbsp;&nbsp;
                                <button className='btn btn-danger' onClick={() => handleDeleteClick(employee.id)}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default EmployeeComponent;
