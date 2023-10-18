using Microsoft.Extensions.Options;
using System.Reflection;
using SudokuApi.Models;
using SudokuApi.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SudokuDatabaseSettings>(
    builder.Configuration.GetSection("SudokuDatabase"));

builder.Services.AddSingleton<PuzzleService>();

builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sudoku with Friends",
        Description = "An ASP.NET Core Web API for Collaborative Sudoku",
        Contact = new OpenApiContact
        {
            Name = "Christa Hansma",
            Url = new Uri("https://github.com/christaha")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

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

