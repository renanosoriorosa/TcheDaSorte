using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using TS.API.Configuration;
using TS.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddJWTConfig(builder.Configuration);

//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc();

builder.Services.ResolveDependencies();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);

app.MapControllers();

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.Run();
