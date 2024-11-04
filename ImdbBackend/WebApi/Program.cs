using Mapster;
using Microsoft.IdentityModel.Tokens;

// Role imports
using DBConnection.Roles;
using DataLayer.Roles;

// Rating imports
using DBConnection.Ratings;
using DataLayer.Ratings;

using DBConnection.Titles;
using DataLayer.Titles;

using DBConnection.Persons;
using DataLayer.Persons;

using DBConnection.TitlePrincipals;
using DataLayer.TitlePrincipals;

using DBConnection.TitleAlternatives;
using DataLayer.TitleAlternatives;

using DBConnection.Users;
using DataLayer.Users;

using DBConnection.Genres;
using DataLayer.Genres;

using DBConnection.Type;
using DataLayer.Types;

using DBConnection.TitleEpisodes;
using DataLayer.TitleEpisodes;

using DBConnection.Searching;
using DataLayer.Searching;

using DataLayer.PersonRoles;
using DBConnection.PersonRoles;

using DataLayer.KnownFors;
using DBConnection.KnownFors;

using DataLayer.Productions;
using DBConnection.Productions;

using DataLayer.Bookmarkings;
using DBConnection.Bookmarkings;

using DataLayer.History;
using DBConnection.History;
using WebApi.Middleware;
using WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config.json");

var connectionString = builder.Configuration.GetSection("ConnectionString").Value ?? "";

// Add services to the container.
builder.Services.AddSingleton<IRoleDataService>(
    serviceProvider => new RoleDataService(connectionString));

builder.Services.AddSingleton<IRatingDataService>(
    serviceProvider => new RatingDataService(connectionString));

builder.Services.AddSingleton<ITitleDataService>(
    serviceProvider => new TitleDataService(connectionString));

builder.Services.AddSingleton<IPersonDataService>(
    serviceProvider => new PersonDataService(connectionString));

builder.Services.AddSingleton<ITitlePrincipalDataService>(
    serviceProvider => new TitlePrincipalDataService(connectionString));

builder.Services.AddSingleton<ITitleAlternativeDataService>(
    serviceProvider => new TitleAlternativeDataService(connectionString));

builder.Services.AddSingleton<IUserDataService>(
    serviceProvider => new UserDataService(connectionString));

builder.Services.AddSingleton<IGenreDataService>(
    serviceProvider => new GenreDataService(connectionString));
    
builder.Services.AddSingleton<ITypeDataService>(
    serviceProvider => new TypeDataService(connectionString));
builder.Services.AddSingleton<ISearchingDataService>(
    serviceProvider => new SearchingDataService(connectionString));

builder.Services.AddSingleton<IPersonRoleDataService>(
    serviceProvider => new PersonRoleDataService(connectionString));
builder.Services.AddSingleton<IKnownForDataService>(
    serviceProvider => new KnownForDataService(connectionString));
builder.Services.AddSingleton<IProductionDataService>(
    serviceProvider => new ProductionDataService(connectionString));
builder.Services.AddSingleton<IBookmarkingDataService>(
    serviceProvider => new BookmarkingDataService(connectionString));
builder.Services.AddSingleton<ITitleEpisodeDataService>(
    serviceProvider => new TitleEpisodeDataService(connectionString));
builder.Services.AddSingleton<IHistoryDataService>(
    serviceProvider => new HistoryDataService(connectionString));
builder.Services.AddSingleton<Hashing>(new Hashing());

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Configure JWT Bearer authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer abcdef12345'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});
builder.Services.AddMapster();

var secret = builder.Configuration.GetSection("Auth:Secret").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        ClockSkew = TimeSpan.Zero

    }
    );

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

app.UseAuth();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
