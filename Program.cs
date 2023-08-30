using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// should remove this, and switch to et 6
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Areas.Identity;

using Serilog;
using Serilog.Events;

using MudBlazor;
using MudBlazor.Services;

using Modules;
using Modules.Middleware;
using Modules.Db;
using Data;
using Data.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config/appsettings.json");
builder.Configuration.AddJsonFile("config/appsettings.Development.json");

// LOGGER

var log = new LoggerConfiguration()
    .Enrich.With(new LoggingOptionEnricher())
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning || evt.Level == LogEventLevel.Error)
        .WriteTo.File("Logs/priority.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
        .WriteTo.File("Logs/error.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Sink(new LoggingEventHandler())
    .CreateLogger();


// USER

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddMudServices();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 8000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// API
builder.Services.AddControllersWithViews(options =>
{
    options.Conventions.Add(new RoutePrefixConvention("api"));
}).AddControllersAsServices();

// CONSTR

#if DEBUG
string ConnectionString = builder.Configuration["Database:ConnectionStringTesting"]!;
#else
string ConnectionString  = builder.Configuration["Database:ConnectionStringProduction"]!;
#endif

// DB TESTING 

#if DEBUG
string TestingTable = "Item1Table";
var con = new TestDbContext(ConnectionString);
log.Information($"Existing Tables: {string.Join(", ", DbHelper.GetExistingTables(con))}");
log.Information($"Table {TestingTable} exists: {DbHelper.CheckTableExists(con, TestingTable)}");
log.Information($"Number of entries in {TestingTable} {DbHelper.CheckNumberEntries(con, TestingTable)}");
#endif

// MODULES

builder.Services.AddSingleton<Constants>();
builder.Services.AddSingleton<FundamentalStorage>();
builder.Services.AddSingleton<MainStorage>();
builder.Services.AddSingleton(provider => log);

builder.Services.AddScoped(provider =>
{
    return new TestDbManager(new TestDbContext(ConnectionString));
});


var app = builder.Build();

// MIDDLEWARE

app.UseMiddleware<LoggingMiddleware>();


// HSTS

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Fundamental/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "static")),
    RequestPath = "/static"
});



app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/Fundamental/_Host");

app.Run();
