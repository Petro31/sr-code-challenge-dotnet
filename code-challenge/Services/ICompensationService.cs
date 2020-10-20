using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public interface ICompensationService
    {
        CompensationResponse GetById(string id);
        CompensationResponse Create(CompensationRequest compensation);
    }
}
