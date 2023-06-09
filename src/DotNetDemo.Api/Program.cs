using DotNetDemo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (Exception)
    {
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsync("An error occurred");
    }
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPIs v1"));

app.UseHttpsRedirection();

// Cors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.RegisterEndpointDefinitions();

app.Run();
