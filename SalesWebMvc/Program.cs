using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Uma parte do Processo para fazer conexão com o Banco de Dados.
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Esse método registra o serviço com um lifetime scoped. Ou seja, uma instância que será criada por requisição HTTP
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();

var app = builder.Build();

//Define que nossa aplicação está no formato de datas e linguagem por padrão em Inglês
var enUS = new CultureInfo("en-US");
var localizationOption = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo>() { enUS },
    SupportedUICultures = new List<CultureInfo>() { enUS }
};

app.UseRequestLocalization(localizationOption);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Usado em casos de "Seeding" (povoamento do banco de dados)
app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
