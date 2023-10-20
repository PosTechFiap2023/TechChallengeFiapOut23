using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentGroupsManager.Data;
using StudentGroupsManager.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentGroupsManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentGroupsManagerContext") ?? throw new InvalidOperationException("Connection string 'StudentGroupsManagerContext' not found.")));

// Add services to the container.

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

app.MapControllers();

app.MapCourseEndpoints();

app.MapStudentEndpoints();

app.MapTeacherCoordinatorEndpoints();

app.Run();
