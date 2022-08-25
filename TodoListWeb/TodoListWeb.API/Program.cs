using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TodoListWeb.API.Data;
using TodoListWeb.API.Repository;
using WebApiContrib.Formatting.Jsonp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.
    AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());


builder.Services.AddDbContext<TodoListDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoList"));
});
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

//Enabling for single domain
//Enabing for multiple domain
//Any domain

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
