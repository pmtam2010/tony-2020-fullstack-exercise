using exercise_project.Models;
using exercise_project.Options;
using exercise_project.Repositories;
using exercise_project.YamlHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercise_project.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<Customer>> ShowCustomerData()
        {
            return await _customerRepo.GetCustomerData();
        }
    }
}
