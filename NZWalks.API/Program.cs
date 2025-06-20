using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mapping;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("NZWalksConnectionString"),new MySqlServerVersion(new Version(8, 3, 0))));

// Registering the repository
builder.Services.AddScoped<IRegionRepo,MySqlRegionRepo>();
builder.Services.AddScoped<IWalkRepo,MySqlIWalkRepo>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
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