﻿using CrossCutting;
using Domain.Commands.v1.Users.CreateUser;
using Domain.Interfaces.Repositories.v1;
using Infraestructure.Data.Repositories.v1;
using Infrastructure.Data.Repositories.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API
{
    internal static class Bootstrapper
    {
        internal static void ConfigureApp(WebApplicationBuilder builder)
        {
            // Adicionar as configurações do AppSettings
            builder.Services.Configure<AppSettingsConfigurations>(builder.Configuration.GetSection(nameof(AppSettingsConfigurations)));
            builder.Services.AddTransient(sp => sp.GetRequiredService<IOptions<AppSettingsConfigurations>>().Value);
            var appSettings = builder?.Services?.BuildServiceProvider()?.GetRequiredService<AppSettingsConfigurations>();

            InjectRepository(builder,appSettings);

            builder.Services.AddMediatR(
                new MediatRServiceConfiguration().RegisterServicesFromAssemblyContaining(typeof(CreateUserCommandHandler)));

            builder.Services.AddAutoMapper(opt => opt.AddMaps(typeof(CreateUserCommandProfile).Assembly));
        }

        private static void InjectRepository(WebApplicationBuilder builder, AppSettingsConfigurations appSettings)
        {
            // Configurar a injeção do IMongoClient
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(appSettings.MongoDBSettings.ConnectionString);
            });

            // Registrar o Repository para injeção de dependência
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
        }
    }
}
