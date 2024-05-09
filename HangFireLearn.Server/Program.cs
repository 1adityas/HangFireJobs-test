using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using HangFireLearn.Producer.Interface;
using HangFireLearn.Server.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage("Server=tcp:sqlserver-hangfire.database.windows.net,1433;Initial Catalog=HangFireDB;Persist Security Info=False;User ID=aditya;Password=@Cr@zy123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"));

builder.Services.AddHangfireServer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<ISendEmailJob, SendEmailJob>();
//services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire", new DashboardOptions{ });



app.Run();
