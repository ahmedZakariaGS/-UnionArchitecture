using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Infrastructure.Persistence
{
    public static class DBSeeder
    {

        public static async Task<bool> SeedDB(ApplicationDbContext context)
        {
            return await SeedData(context);
        }

        private static async Task<bool> SeedData(ApplicationDbContext context)
        {

           
            if (!context.users.Any())
            {
                context.users.Add(
                    new User {Id=0, Name= "ahmed zakaria" }
                );
            }
          
       
            await context.SaveChangesAsync();
            return true;


        }   
    }
}
