﻿namespace Elibrary.API.Extensions;

public static class ServiceExtensions
{

    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryServiceManager, RepositoryServiceManager>();
        services.AddScoped(serviceType: typeof(IUnitOfWork), implementationType: typeof(UnitOfWork));

        services.AddScoped(serviceType: typeof(ICoreRepo<>), implementationType: typeof(CoreRepo<>));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "E Library",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Kazeem Quadri",
                    Email = "quadrikazeem01@gmail.com",
                    Url = new Uri("https://kaz.com.ng"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/DarkAce01/elibrary/blob/main/LICENSE"),
                }
            });

            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            swagger.OperationFilter<SwaggerFileOperationFilter>();
        });
    }
}
