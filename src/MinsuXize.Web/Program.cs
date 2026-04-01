using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using MinsuXize.Web.Data;
using MinsuXize.Web.Data.Seed;
using MinsuXize.Web.Options;
using MinsuXize.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var databaseDirectory = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(databaseDirectory);

var databasePath = Path.Combine(databaseDirectory, "minsuxize.db");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? $"Data Source={databasePath}";

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});
builder.Services.Configure<AdminAuthOptions>(builder.Configuration.GetSection("AdminAuth"));
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/manage/login";
        options.AccessDeniedPath = "/manage/login";
        options.Cookie.Name = "minsuxize.admin";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("AdminAccess", "true"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IFolkloreRepository, EfFolkloreRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    DbSeeder.Seed(dbContext);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseForwardedHeaders();

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    await next();
});

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Home/Status", "?code={0}");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapHealthChecks("/healthz");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
