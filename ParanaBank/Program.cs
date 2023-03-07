using Microsoft.Extensions.Configuration;
using ParanaBank.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSqlServerConnection(configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDependencyResolver();

builder.Services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

builder.Services.AddMediatRApi();

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
