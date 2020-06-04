using exercise_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercise_project.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> ShowCustomerData();
    }
}
