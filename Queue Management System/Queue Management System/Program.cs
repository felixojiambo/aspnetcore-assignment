using Queue_Management_System.Data;
using Queue_Management_System.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<ServicePointRepository>();
builder.Services.AddScoped<ServiceProviderRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<UserRepository>(sp => new UserRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

