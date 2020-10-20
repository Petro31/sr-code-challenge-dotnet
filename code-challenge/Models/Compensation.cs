using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        [Key]
        [JsonIgnore]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public String Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
