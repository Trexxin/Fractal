using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context) 
        {
            // Checks to see if the users table is populated
            if (await context.Users.AnyAsync()) return;

            // Grabs the data from UserSeedData.json and stores it in userData
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            // Deserializes the data in userData and passes the data into users
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            // Adds each user to the database and assigns them a password
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$word"));
                user.PasswordSalt = hmac.Key;

                // Adds tracking to the user in Entity Framework
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}