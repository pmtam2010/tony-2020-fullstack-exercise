using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DALexercise_project.Repositories
{
    public interface IAuthenticationRepo
    {
        Task<bool> Login(string username, string password);
    }
}
