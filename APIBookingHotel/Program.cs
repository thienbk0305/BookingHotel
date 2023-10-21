using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<BookingHotelDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnStr"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddIdentity<User, IdentityRole>().
    AddEntityFrameworkStores<BookingHotelDbContext>().AddDefaultTokenProviders();
//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

//Dependency Injection
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddTransient<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddTransient<IBookingHotelUnitOfWork, BookingHotelUnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
