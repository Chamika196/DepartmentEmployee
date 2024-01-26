// App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import DepartmentComponent from './features/components/department/DepartmentComponent';
import EmployeeComponent from './features/components/employee/EmployeeComponent';
import './App.css';
const App = () => {
  return (
    <Router>
      <div className="navbar">
        
        <nav>
          <ul className="nav-list">
            <li className="nav-item">
              <Link to="/departments" className="nav-link">Departments</Link>
            </li>
            <li className="nav-item">
              <Link to="/employees" className="nav-link">Employees</Link>
            </li>
          </ul>
        </nav>
      </div>

      <hr />

      <Routes>
        <Route path="/departments" element={<DepartmentComponent />} />
        <Route path="/employees" element={<EmployeeComponent />} />
      </Routes>
    </Router>
  );
};

export default App;
