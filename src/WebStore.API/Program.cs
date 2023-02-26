using WebStore.API.Configurations;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Helpers;
using WebStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserSession, UserSession>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureInfrastructureService(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.ConfigureCors();

builder.Services.ConfigureCookies();

builder.Services.RegisterService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("__cors");

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
