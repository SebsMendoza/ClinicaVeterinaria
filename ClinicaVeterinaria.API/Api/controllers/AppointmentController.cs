﻿using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using System.Text.Json;

namespace ClinicaVeterinaria.API.Api.controllers
{
    [ExtendObjectType(typeof(Query))]
    public class AppointmentController
    {
        private readonly AppointmentService Service;

        public AppointmentController(AppointmentService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return JsonSerializer.Serialize(Results.Json(data: task.Result, statusCode: 200));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindById(Guid id)
        {
            var task = Service.FindById(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string Create(Appointment appointment)
        {
            var task = Service.Create(appointment);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 201),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string Delete(Guid id)
        {
            var task = Service.Delete(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }
    }
}
