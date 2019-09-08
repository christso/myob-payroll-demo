using MYOB.Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYOB.Payroll.Data
{
    public class EmployeeRepository
    {
        private PayrollContext _context;

        public EmployeeRepository(PayrollContext context)
        {
            _context = context;
        }

        public void AddDefaultData()
        {
            var emps = _context.Employees;
            if (emps.Count() == 0)
            {
                _context.Add(new Employee()
                {
                    AnnualSalary = 60500,
                    FirstName = "Christopher",
                    LastName = "Tso",
                    Id = Guid.NewGuid().ToString()
                });
            }
            _context.SaveChanges();
        }

        public IEnumerable<Employee> FindAll()
        {
            return _context.Employees.ToList();
        }
    }
}
