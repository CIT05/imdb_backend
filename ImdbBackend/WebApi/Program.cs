using Mapster;

// Role imports
using DBConnection.Roles;
using DataLayer.Roles;

// Rating imports
using DBConnection.Ratings;
using DataLayer.Ratings;

using DBConnection.Titles;
using DataLayer.Titles;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");

var connectionString = builder.Configuration.GetSection("ConnectionString").Value ?? "";

// Add services to the container.
builder.Services.AddSingleton<IRoleRepository>(
    serviceProvider => new RoleRepository(connectionString));

builder.Services.AddSingleton<IRatingRepository>(
    serviceProvider => new RatingRepository(connectionString));

builder.Services.AddSingleton<ITitleRepository>(
    serviceProvider => new TitleRepository(connectionString));

// scoped means that the service is created once per request
builder.Services.AddScoped<IRoleDataService, RoleDataService>();
builder.Services.AddScoped<IRatingDataService, RatingDataService>();
builder.Services.AddScoped<ITitleDataService, TitleDataService>();



builder.Services.AddMapster();

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

//app.UseAuthorization();

app.MapControllers();

app.Run();
