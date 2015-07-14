using Cus.Interfaces.Order;
using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Services.Order
{
    public class EmployeeService
    {
        private IEmployee _Employee = null;
        public EmployeeService(IEmployee Employee)
        {
            _Employee = Employee;
        }

        public IEnumerable<Employees> GetEmployees()
        {
            return _Employee.GetEmployees();
        }
    }
}
