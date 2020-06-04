using exercise_project.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace exercise_project.Repositories
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetCustomerData();
    }
}
