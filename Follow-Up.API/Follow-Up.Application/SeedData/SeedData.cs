using Follow_Up.Domain;
using Follow_Up.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Follow_Up.Application.SeedData
{
    public static class SeedData
    {
        public static async Task DatabaseMigrator(this FollowUpDbContext dbContext)
        {
            await dbContext.Database.MigrateAsync();
            await SeedDataCreate(dbContext);
        }
        public static async Task SeedDataCreate(FollowUpDbContext dbContext)
        {
            try
            {
                if (await dbContext.Users.CountAsync() > 0) return;
                var user = new User
                {
                    CreateDate = DateTime.Now,
                    Email = "fatih.mandirali@hotmail.com",
                    IsActive = true,
                    IsDeleted = false,
                    ModifiedDate = DateTime.Now,
                    Name = "Fatih",
                    Password = BC.HashPassword("123456"),
                    Phone = "5393551932",
                    Surname = "Mandıralı"
                };
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception err)
            {

                throw err;
            }
            
        }
    }
}
