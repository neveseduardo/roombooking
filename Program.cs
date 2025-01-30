using RoomBooking.Database;
using RoomBooking.Repository;
using RoomBooking.Extensions;
using RoomBooking.Repositories;
using YourNamespace.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddCustomSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.MapControllers();
app.SeedDatabase();

app.Run();
