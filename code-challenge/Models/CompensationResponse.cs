using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class CompensationResponse
    {
        public Employee Employee { get; set; }
        public String Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
