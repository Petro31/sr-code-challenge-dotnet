using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using System.Security.Cryptography.X509Certificates;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }
        public ReportingStructure GetReportingStructureById(string id)
        {
            //_employeeContext.Employees.Load();

            List<string> reports = new List<string>();
            getReports(id, reports);
            return new ReportingStructure() { Employee = GetById(id),  NumberOfReports = reports.Count() };
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        private void getReports(string id, List<string> employees = null)
        {
            if (employees == null)
                employees = new List<string>();

            _employeeContext.Employees
                    .Where(e => e.EmployeeId == id)
                    .SelectMany(x => x.DirectReports)
                    .Select(e => e.EmployeeId)
                    .ToList().ForEach(report =>
                    {
                        employees.Add(report);
                        getReports(report, employees);
                    }
                );
        }

    }
}
