using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] CompensationRequest compensationRequest)
        {
            var employee = _employeeService.GetById(compensationRequest.EmployeeId);

            if (employee == null)
            {
                return NotFound();
            }
            _logger.LogDebug($"Received compensation create request for '{compensationRequest.EmployeeId}'");

            var compensationResponse = _compensationService.Create(compensationRequest);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = employee.EmployeeId }, compensationResponse);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(string id)
        {
            _logger.LogDebug($"Recieved compensation get request for employee '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
            {
                return NotFound();
            }
            return Ok(compensation);

        }
    }
}
