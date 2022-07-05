using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        // Post Request to register a new user
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) 
        {
            // Checks to see if username has been taken
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("Username is taken");
            }

            using var hmac = new HMACSHA512();

            var user = new AppUser 
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            // Enables Entity Framework to track new user
            _context.Users.Add(user);
            // Adds new user to the database asynchronously
            await _context.SaveChangesAsync();

            return user;
        }

        // Ensures each username in the database is unique
        private async Task<bool> UserExists(string UserName) 
        {
            return await _context.Users.AnyAsync(u => u.UserName == UserName.ToLower());
            
        }
    }
}