﻿using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API.Api.repositories
{
    public class AppointmentRepository
    {
        private readonly IDbContextFactory<ClinicaDBContext> ContextFactory;

        public AppointmentRepository(IDbContextFactory<ClinicaDBContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<List<Appointment>> FindAll()
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var appointment = await context.Appointments.ToListAsync();
            return appointment ?? new();
        }

        public async Task<Appointment?> FindById(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            return await context.Appointments.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Appointment> Create(Appointment appointment)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment?> Update(Guid id, Appointment appointment)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var found = context.Appointments.FirstOrDefault(u => u.Id == id);
            if (found != null)
            {
                appointment.Id = found.Id;
                context.Appointments.Update(appointment);
                await context.SaveChangesAsync();

                return appointment;
            }
            return null;
        }

        public async Task<Appointment?> Delete(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var found = context.Appointments.FirstOrDefault(u => u.Id == id);
            if (found != null)
            {
                context.Appointments.Remove(found);
                await context.SaveChangesAsync();

                return found;
            }
            return null;
        }
    }
}
