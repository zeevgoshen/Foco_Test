using DataAccess.Data;
using FocoTest.Services.Tests;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    //builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddScoped<ITestService, TestService>();
    builder.Services.AddScoped<ISmsService, SmsService>();
    builder.Services.AddDbContext<DataContext>(
        o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
}


var app = builder.Build();
{
     
    //app.UseSwagger();
    //app.UseSwaggerUI();
     

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

