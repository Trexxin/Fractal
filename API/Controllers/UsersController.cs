using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // Protects methods with authorization
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Endpoint to get all users in database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        // Endpoint to get an individual user by their id
        [HttpGet("{UserName}")]
        public async Task<ActionResult<AppUser>> GetUsers(string UserName) 
        {
            return await _userRepository.GetUserByUsernameAsync(UserName);
        }
    }
}