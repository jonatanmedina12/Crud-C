using Data.Interfaces;
using Data.Servicios;
using Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errores;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Utilidades;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {

                public static IServiceCollection AgregarServicioAplicacion (this IServiceCollection services,IConfiguration config) {

                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen(options =>
                    {
                        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "Ingresar Bearer [espacion] token \r\n\r\n " +
                                            "Ejemplo: Berear ejoy^8878899999990000",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Scheme = "Bearer"
                        });
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme ="oauth2",
                        Name ="Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
                    });

                    var connectionString = config.GetConnectionString("DefaultConnection");
                    services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(connectionString));

                    services.AddCors();

                    services.AddScoped<ITokenServicio, TokenServicio>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionsContext =>
                {
                    var errores = actionsContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                }; ;
            });
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
                }
    }
}
