using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;
        public CompensationService(ILogger<CompensationService> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public CompensationResponse Create(CompensationRequest compensationRequest)
        {
            if (compensationRequest != null)
            {
                Compensation compensation = new Compensation() 
                { 
                    EmployeeId = compensationRequest.EmployeeId,
                    Salary = compensationRequest.Salary, 
                    EffectiveDate = compensationRequest.EffectiveDate 
                };
                _compensationRepository.Add(compensation);
                addEmployee(compensation);
                _compensationRepository.SaveAsync().Wait();
                return response(compensation);
            }
            return null;
        }

        public CompensationResponse GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Compensation compensation = _compensationRepository.GetById(id);
                addEmployee(compensation);
                if (compensation.Employee != null)
                {
                    return response(compensation);
                }
            }
            return null;
        }
        private CompensationResponse response(Compensation compensation)
        {
            if (compensation != null)
            {
                return new CompensationResponse()
                {
                    Employee = compensation.Employee,
                    Salary = compensation.Salary,
                    EffectiveDate = compensation.EffectiveDate,
                };
            }
            return null;
        }
        private Compensation addEmployee(Compensation compensation)
        {
            if (compensation.Employee == null)
            {
                Employee employee = _employeeRepository.GetById(compensation.EmployeeId);
                if (employee != null)
                {
                    compensation.Employee = employee;
                }
            }
            return compensation;
        }
    }
}
