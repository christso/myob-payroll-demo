using MYOB.Payroll.Models;
using MYOB.Payroll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYOB.Payroll.Utils
{
    public class PayslipUtil
    {
        public Payslip CreatePayslip(
            PaySlipRequest request, 
            Employee employee,
            IEnumerable<IncomeTaxRate> rates)
        {
            var payslip = new Payslip();
            payslip.PeriodEnd = GetMonthEndDate(request.PaymentStartDate);
            payslip.GrossIncome = Math.Floor(employee.AnnualSalary / 12);
            payslip.IncomeTax = Math.Ceiling(CalculateTax(employee.AnnualSalary, rates) / 12);
            payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
            payslip.Super = Math.Floor(payslip.GrossIncome * request.SuperRate);
            
            return payslip;
        }

        public decimal CalculateTax(decimal annualSalary, IEnumerable<IncomeTaxRate> rates)
        {
            var upperIncome = rates.Where(r => annualSalary <= r.UpperIncome).Min(r => r.UpperIncome);
            var applicableRates = rates.Where(r => r.UpperIncome == upperIncome);
            if (applicableRates.Count() > 1)
            {
                throw new InvalidOperationException("Tax rate table cannot have more than one rate for a salary range");
            }
            var rate = applicableRates.FirstOrDefault();
            if (rate == null)
            {
                throw new InvalidOperationException("No matching tax rate.");
            }

            return rate.BaseTax + rate.MultiplierTax * (annualSalary - rate.LowerIncome);
        }

        public DateTime GetMonthEndDate(DateTime theDate)
        {
            return theDate.AddMonths(1).AddDays(-1);
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
