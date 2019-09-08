using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.Payroll;
using MYOB.Payroll.Models;
using MYOB.Payroll.Utils;
using MYOB.Payroll.ViewModels;
using System;

namespace MYOB.Payroll.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPayslip()
        {
            var util = new PayslipUtil();
            var empId = Guid.NewGuid().ToString();
            var employee = new Employee()
            {
                Id = empId,
                FirstName = "Christopher",
                LastName = "Tso",
                AnnualSalary = 100000m
            };
            var request = new PaySlipRequest()
            {
                EmployeeId = empId,
                PaymentStartDate = new DateTime(2019, 3, 1),
                SuperRate = 0.08m
            };
            var payslip = util.CreatePayslip(request, employee);
            Assert.AreEqual(employee.AnnualSalary / 12, payslip.GrossIncome);
        }
    }
}
