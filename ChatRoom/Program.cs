using ChatRoom;
using ChatRoom.Datos;
using ChatRoom.Auth.Models;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Identity;
using ChatRoom.Datos.Models;
using User = ChatRoom.Auth.Models.User;
using ChatRoom.Datos.Entidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<SubastaContext>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IOfertumService, OfertumService>();

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6; 
    options.Password.RequiredUniqueChars = 0; 

    // Otras configuraciones de Identity, si las tienes
})
    .AddEntityFrameworkStores<SubastaContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";
});

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
    pattern: "{controller=Chat}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chat");

app.Run();
