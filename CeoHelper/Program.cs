using CeoHelper.Data;
using CeoHelper.Services;
using CeoHelper.Shared.Options;
using CeoHelper.Web.Validators;
using CeoHelper.Web.Validators.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
var _configuration = new ConfigurationBuilder()
    .AddJsonFile($"secrets.{Environment.GetEnvironmentVariable("ASPNETCORE_URLS")}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();
builder.Services.AddSingleton(_configuration);
var googleOAuthSettings = _configuration.GetSection(nameof(GoogleOAuthSettings));
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = googleOAuthSettings.GetValue<string>(nameof(GoogleOAuthSettings.ClientID));
    googleOptions.ClientSecret = googleOAuthSettings.GetValue<string>(nameof(GoogleOAuthSettings.ClientSecret));
});
builder.Services.ConfigureBusinessLogic(_configuration);
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU")
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    builder.Configuration.AddUserSecrets<Program>();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.UseStatusCodePages(ctx =>
{
    if (ctx.HttpContext.Response.StatusCode == 404)
        ctx.HttpContext.Response.Redirect("/Home/Index");

    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
