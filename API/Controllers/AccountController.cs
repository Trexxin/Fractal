using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        // Post Request to register a new user
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
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

            return new UserDto 
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        // Post Request to allow a user to login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // Searches for username in database
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            // Gets the password hash using the PasswordSalt as a key
            using var hmac = new HMACSHA512(user.PasswordSalt);
            // Creats password hash using the inputed password
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Checks if the inputed password is the same as the one stored in the database
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            return new UserDto 
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        // Ensures each username in the database is unique
        private async Task<bool> UserExists(string UserName) 
        {
            return await _context.Users.AnyAsync(u => u.UserName == UserName.ToLower());
            
        }
    }
}