using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class CompensationRequest
    {
        public string EmployeeId { get; set; }
        public string Salary { get; set; }
        public string EffectiveDate { get; set; }
    }
}
