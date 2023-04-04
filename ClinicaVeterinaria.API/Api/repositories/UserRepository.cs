﻿using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API.Api.repositories
{
    public class UserRepository
    {
        private readonly IDbContextFactory<ClinicaDBContext> ContextFactory;

        public UserRepository(IDbContextFactory<ClinicaDBContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<List<User>> FindAll()
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var users = await context.Users.ToListAsync();
            return users ?? new();
        }

        public async Task<User?> FindById(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Create(User user)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Update(Guid id, User user)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var found = context.Users.FirstOrDefault(u => u.Id == id);
            if (found != null)
            {
                user.Id = found.Id;
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return user;
            }
            return null;
        }

        public async Task<User?> Delete(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var foundUser = context.Users.FirstOrDefault(u => u.Id == id);
            if (foundUser != null)
            {
                context.Users.Remove(foundUser);
                await context.SaveChangesAsync();

                return foundUser;
            }
            return null;
        }
    }
}
