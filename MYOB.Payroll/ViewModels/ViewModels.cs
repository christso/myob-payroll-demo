﻿using MYOB.Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYOB.Payroll.ViewModels
{
    public class PaySlipRequest : IdentityObject
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal SuperRate { get; set; }
        public DateTime PaymentStartDate { get; set; }
    }
}
