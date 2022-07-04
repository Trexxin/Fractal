using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // Endpoint to get all users in database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            return await _context.Users.ToListAsync();
        }

        // Endpoint to get an individual user by their id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id) 
        {
            return await _context.Users.FindAsync(id);
        }
    }
}