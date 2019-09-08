using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYOB.Payroll;
using MYOB.Payroll.Models;
using MYOB.Payroll.Utils;
using MYOB.Payroll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
                AnnualSalary = 60050m
            };
            var request = new PaySlipRequest()
            {
                EmployeeId = empId,
                PaymentStartDate = new DateTime(2019, 3, 1),
                SuperRate = 0.09m
            };

            var taxRates = GetDefaultIncomeTaxRates();

            var payslip = util.CreatePayslip(request, employee, taxRates);
            Assert.AreEqual(new DateTime(2019, 3, 31), payslip.PeriodEnd);
            Assert.AreEqual(Math.Floor(employee.AnnualSalary / 12), payslip.GrossIncome);

            // TODO: fix bug

            Assert.AreEqual(Math.Ceiling(3572 + 0.325m * (employee.AnnualSalary - 37000)) / 12,
                payslip.IncomeTax);
            Assert.AreEqual(payslip.GrossIncome - payslip.IncomeTax, payslip.NetIncome);
            Assert.AreEqual(Math.Floor(payslip.GrossIncome * request.SuperRate), payslip.Super);
        }

        public IList<IncomeTaxRate> GetDefaultIncomeTaxRates()
        {
            var rates = new List<IncomeTaxRate>();
            rates.Add(new IncomeTaxRate()
            {
                LowerIncome = 0,
                UpperIncome = 18200,
                BaseTax = 0,
                MultiplierTax = 0
            });
            rates.Add(new IncomeTaxRate()
            {
                LowerIncome = 18200,
                UpperIncome = 37000,
                BaseTax = 0,
                MultiplierTax = 0.19m
            });
            rates.Add(new IncomeTaxRate()
            {
                LowerIncome = 37000,
                UpperIncome = 87000,
                BaseTax = 3572,
                MultiplierTax = 0.325m
            });
            rates.Add(new IncomeTaxRate()
            {
                LowerIncome = 87000,
                UpperIncome = 180000,
                BaseTax = 19822,
                MultiplierTax = 0.37m
            });
            rates.Add(new IncomeTaxRate()
            {
                LowerIncome = 180000,
                UpperIncome = Decimal.MaxValue,
                BaseTax = 54232,
                MultiplierTax = 0.45m
            });

            return rates;
        }
    }
}
