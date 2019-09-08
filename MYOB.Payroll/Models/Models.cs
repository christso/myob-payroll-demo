using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace MYOB.Payroll.Models
{
    public class IdentityObject
    {
        public IdentityObject()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }

    public class Employee : IdentityObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualSalary { get; set; }
    }

    public class IncomeTaxRate
    {
        public string Id { get; set; }
        public decimal LowerIncome { get; set; }
        public decimal UpperIncome { get; set; }
        public decimal BaseTax { get; set; }
        public decimal MultiplierTax { get; set; }
    }

    public class Payslip
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }
    }
}
