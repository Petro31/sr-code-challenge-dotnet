using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Data;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext context)
        {
            _compensationContext = context;
            _logger = logger;
        }
        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }
        public Compensation GetById(string id)
        {
            return _compensationContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
