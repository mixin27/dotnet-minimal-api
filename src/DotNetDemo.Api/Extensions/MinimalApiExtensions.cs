using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess;
using DataAccess.Repositories;
using DotNetDemo.Api.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DotNetDemo.Api.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            var cs = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));

            builder.Services.AddScoped<IPostRepository, PostRepository>();

            // version < 12
            // builder.Services.AddMediatR(typeof(CreatePost));
            // version >= 12
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePost).Assembly));
            builder.Services.AddMediatR(mrsc => mrsc.RegisterServicesFromAssembly(typeof(CreatePost).Assembly));
        }

        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes().Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDef in endpointDefinitions)
            {
                endpointDef.RegisterEndpoints(app);
            }
        }
    }
}
