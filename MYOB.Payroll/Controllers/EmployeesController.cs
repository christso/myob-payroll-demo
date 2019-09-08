using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYOB.Payroll.Data;
using MYOB.Payroll.Models;

namespace MYOB.Payroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeRepository.AddDefaultData();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(string id)
        {
            try
            {
                var emp = _employeeRepository.FindAll().FirstOrDefault(x => x.Id.Equals(id));
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            try
            {
                var emps = _employeeRepository.FindAll();
                return Ok(emps);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
