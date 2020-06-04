using DALexercise_project.Repositories;
using exercise_project.Models;
using exercise_project.Options;
using exercise_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace exercise_project.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepo _authRepo;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService() { }

        public AuthenticationService(IAuthenticationRepo authRepo, JwtSettings jwtSettings)
        {
            _authRepo = authRepo;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationViewModel> Login(string username, string password)
        {
            var isAuthenticated = await _authRepo.Login(username, password);
            if (!isAuthenticated)
            {
                return new AuthenticationViewModel
                {
                    Errors = new[] { "User does not exist" }
                };
            }
            return GenerateAuthenticationResultForUser(username);
        }

        private AuthenticationViewModel GenerateAuthenticationResultForUser(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[] {
                    new Claim(ClaimTypes.Name, username, ClaimValueTypes.String)
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationViewModel
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };

        }
    }
    }
