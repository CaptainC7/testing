using Microsoft.EntityFrameworkCore;
using ClassLibraryDLL.Models.ApplicationDBContext;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonServices, PersonServices>();
builder.Services.AddScoped<ITaskListTemplateServices, TaskListTemplateServices>();
builder.Services.AddScoped<ITaskGroupServices, TaskGroupServices>();
builder.Services.AddScoped<ITaskServices, TaskServices>();
builder.Services.AddScoped<ITaskListInstanceServices, TaskListInstanceServices>();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDb")));

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

app.Run();
