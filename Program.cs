using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using HangFireLearn.Producer.Interface;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Add Hangfire services.

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage("Server=tcp:sqlserver-hangfire.database.windows.net,1433;Initial Catalog=HangFireDB;Persist Security Info=False;User ID=aditya;Password=@Cr@zy123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"));

// Add the processing server as IHostedService
//builder.Services.AddHangfireServer();

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

app.UseAuthorization();

app.UseHangfireDashboard();
//app.MapHangfireDashboard();

RecurringJob.AddOrUpdate(Guid.NewGuid().ToString(),
     () => Console.WriteLine("recurring job"), Cron.Minutely);

app.MapControllers();

app.Run();
