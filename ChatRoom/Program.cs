using ChatRoom;
using ChatRoom.Datos;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Identity;
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
    })
    .AddEntityFrameworkStores<SubastaContext>()
    .AddDefaultTokenProviders();

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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chat}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chat");

app.Run();
