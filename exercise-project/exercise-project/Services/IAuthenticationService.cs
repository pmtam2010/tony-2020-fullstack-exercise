using exercise_project.Models;
using exercise_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercise_project.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationViewModel> Login(string username, string password);
    }
}
