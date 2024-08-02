using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conexao = builder.Configuration
    .GetConnectionString("PokedexConexao");
var versao= ServerVersion.AutoDetect(conexao);
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseMySql(conexao, versao)
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
