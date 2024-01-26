import React, { useState, useEffect } from 'react';
import axios from 'axios';

const DepartmentComponent = () => {
  const [departments, setDepartments] = useState([]);
  const [newDepartment, setNewDepartment] = useState({ depCode: '', depName: '' });
  const [selectedDepartment, setSelectedDepartment] = useState(null);

  useEffect(() => {
    // Fetch the list of departments on component mount
    fetchDepartments();
  }, []);

  const fetchDepartments = async () => {
    try {
      const response = await axios.get('https://localhost:7239/api/Department');
      setDepartments(response.data);
    } catch (error) {
      console.error('Error fetching departments:', error);
    }
  };

  const handleInputChange = (e) => {
    // Update selectedDepartment if it exists
    setSelectedDepartment((prevSelectedDepartment) => {
      if (prevSelectedDepartment) {
        return {
          ...prevSelectedDepartment,
          [e.target.name]: e.target.value,
        };
      }
      return null;
    });
  
    // Update newDepartment if selectedDepartment is null
    if (!selectedDepartment) {
      setNewDepartment((prevNewDepartment) => ({
        ...prevNewDepartment,
        [e.target.name]: e.target.value,
      }));
    }
  };
  

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.post('https://localhost:7239/api/Department', newDepartment);
      fetchDepartments();
      setNewDepartment({ depCode: '', depName: '' });
    } catch (error) {
      console.error('Error adding department:', error);
    }
  };

  const handleEditClick = async (id) => {
    try {
      const response = await axios.get(`https://localhost:7239/api/Department/${id}`);
      const fetchedDepartment = response.data;

      setSelectedDepartment({
        id: fetchedDepartment.id,
        depCode: fetchedDepartment.depCode,
        depName: fetchedDepartment.depName,
      });
    } catch (error) {
      console.error('Error fetching department for edit:', error);
    }
  };

  const handleEditSubmit = async () => {
    try {
      await axios.put(`https://localhost:7239/api/Department/${selectedDepartment.id}`, {
        depCode: selectedDepartment.depCode,
        depName: selectedDepartment.depName,
      });

      setDepartments((prevDepartments) =>
        prevDepartments.map((department) =>
          department.id === selectedDepartment.id
            ? {
                ...department,
                depCode: selectedDepartment.depCode,
                depName: selectedDepartment.depName,
              }
            : department
        )
      );

      setSelectedDepartment(null);
    } catch (error) {
      console.error('Error editing department:', error);
    }
  };

  const handleDeleteClick = async (Id) => {
    try {
      const response = await axios.delete(`https://localhost:7239/api/Department/${Id}`);
      fetchDepartments();
    } catch (error) {
      console.error('Error deleting department:', error);
    }
  };

  return (
    <div className='container'>
      <div className='card mt-3'>
        <div className='card-body'>
          <div className='row justify-content-center'>
            <h5 className='card-title text-center'>Departments</h5>
          </div>
          <form onSubmit={selectedDepartment ? handleEditSubmit : handleSubmit}>
            <div className='row'>
              <div className='col-md-6'>
                <div className='mb-3'>
                  <label className='form-label'>Department Code:</label>
                  <input
                    type='text'
                    className='form-control'
                    name='depCode'
                    value={selectedDepartment ? selectedDepartment.depCode : newDepartment.depCode}
                    onChange={handleInputChange}
                  />
                </div>
              </div>
              <div className='col-md-6'>
                <div className='mb-3'>
                  <label className='form-label'>Department Name:</label>
                  <input
                    type='text'
                    className='form-control'
                    name='depName'
                    value={selectedDepartment ? selectedDepartment.depName : newDepartment.depName}
                    onChange={handleInputChange}
                  />
                </div>
              </div>
            </div>
            <button type='submit' className='btn btn-success'>
              {selectedDepartment ? 'Edit Department' : 'Add Department'}
            </button>
          </form>
        </div>
      </div>

      <table className='table table-dark table-hover mt-4'>
        <thead>
          <tr>
            <th scope='col'>Department ID</th>
            <th scope='col'>Department Code</th>
            <th scope='col'>Department Name</th>
            <th scope='col'>Actions</th>
          </tr>
        </thead>
        <tbody>
          {departments.map((department) => (
            <tr key={department.id}>
              <td>{department.id}</td>
              <td>{department.depCode}</td>
              <td>{department.depName}</td>
              <td>
                <button className='btn btn-warning' onClick={() => handleEditClick(department.id)}>
                  Edit
                </button>
                &nbsp;&nbsp;
                <button className='btn btn-danger' onClick={() => handleDeleteClick(department.id)}>
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

export default DepartmentComponent;
