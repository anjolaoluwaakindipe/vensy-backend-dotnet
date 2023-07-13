
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Vensy.Api;
using Vensy.Infrastructure;
using Vensy.Application;
using Vensy.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("dafs"));
builder.Services.AddAPI().AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
