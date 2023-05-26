using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SARITASA.DataAccess;
using SARITASA.Entity;
using SARITASA.Sevices.Implementations;
using SARITASA.Sevices;
using SARITASA.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Add Database

builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SARITASA.API")));



builder.Services.AddIdentity<User, IdentityRole<Guid>>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               //.AddUserStore<UserStore>()
               .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.ConfigureJwt(builder.Configuration);

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
