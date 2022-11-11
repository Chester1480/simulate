using CIMSimulate.Service;
using CIMSimulate.Service.UtilS;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<CIMService>();
builder.Services.AddSingleton<GreenTransService>();

builder.Services.AddSingleton<CacheService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<SoapService>();
builder.Services.AddSingleton<HttpService>();
builder.Services.AddSingleton<SeleniumService>();
builder.Services.AddSingleton<VerifyService>();

builder.Services.AddControllers().AddXmlSerializerFormatters();

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
