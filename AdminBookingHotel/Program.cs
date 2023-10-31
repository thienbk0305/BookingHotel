using DataAccess.DBContext;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DataAccess.Permission;
using Microsoft.AspNetCore.Authorization;
using DataAccess.UnitOfWork;
using DataAccess.IRepositories;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookingHotelDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnStr"));
});

builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.BottomLeft
}).AddNewtonsoftJson(optiton =>
{
    optiton.SerializerSettings.ContractResolver = new DefaultContractResolver();
    optiton.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddIdentity<User, IdentityRole>().
    AddEntityFrameworkStores<BookingHotelDbContext>().AddDefaultTokenProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IUtilitiesRepository<>), typeof(UtilitiesRepository<>));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
