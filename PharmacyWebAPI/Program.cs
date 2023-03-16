using Microsoft.EntityFrameworkCore;
using PharmacyWebAPI.DataAccess;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using PharmacyWebAPI.Utility;
using e_Tickets.Data;
using PharmacyWebAPI.Utility.Services.IServices;
using PharmacyWebAPI.Utility.Services;
using PharmacyWebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Extensions services to the container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}");
await ApplicationDbInitializer.Seed(app);
app.Run();