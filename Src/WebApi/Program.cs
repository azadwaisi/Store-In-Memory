using WebApi;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.SeedData;
using Application;
using Application.Contracts;
using WebApi.Middleware;
using Asp.Versioning;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Infrastructure.Services;
using StackExchange.Redis;
using Domain.Entities.Users;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Identity;
using Infrastructure.Services.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//My Configurations
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddWebServiceCollection();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddApiVersioning(options =>
{
	// Report API versions in response headers (recommended for discoverability)
	options.ReportApiVersions = true;

	// Default version if not specified.  Important!
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0); // e.g., v1.0

	// Choose your versioning methods (combine as needed)
	options.ApiVersionReader = ApiVersionReader.Combine(
		new UrlSegmentApiVersionReader(), // URL Path
		new QueryStringApiVersionReader("api-version"), // Query String (customizable parameter name)
		new HeaderApiVersionReader("X-Api-Version")  // Header (customizable header name)
													 //new MediaTypeApiVersionReader("version") //For Media Type Versioning, need to specify media type in Accept Header
	);
})
.AddApiExplorer(options =>
{
	// Add the versioned API explorer, which adds support for Swagger/OpenAPI
	options.GroupNameFormat = "'v'VVV"; // Format the version as "v[major].[minor]" in Swagger

	// Substitute the version in the URL template for Swagger
	options.SubstituteApiVersionInUrl = true;
});

// 1. پیکربندی JwtSettings
//var jwtSettings = new JwtSettings() { Issuer = "www" , Audience = "wwww" , Secret = "wwwwww" };
//builder.Configuration.Bind(JwtSettings.SectionName, jwtSettings);
var jwtSettingsSection = builder.Configuration.GetSection(JwtSettings.SectionName);
if (!jwtSettingsSection.Exists())
{
	throw new InvalidOperationException(
		$"JWT Settings section '{JwtSettings.SectionName}' is missing in the configuration. " +
		"Please ensure it is defined in appsettings.json or other configuration sources.");
}

// Get<JwtSettings>() یک نمونه ایجاد کرده و آن را bind می‌کند.
// اگر پراپرتی‌های required در appsettings.json موجود نباشند، اینجا یک Exception رخ خواهد داد.
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

// بررسی اینکه آیا Get<T> موفق به ایجاد نمونه شده است (در صورت عدم وجود بخش یا خطای bind ممکن است null باشد،
// هرچند با required باید exception بدهد اگر فیلدها نباشند)
if (jwtSettings == null)
{
	throw new InvalidOperationException(
		$"Could not bind JWT Settings from section '{JwtSettings.SectionName}'. " +
		"Ensure all required fields (Secret, Issuer, Audience) are present and correctly configured.");
}
// ثبت JwtSettings برای استفاده با IOptions<JwtSettings> (روش استاندارد)
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

// همچنین می‌توانید نمونه bind شده را مستقیماً به عنوان Singleton ثبت کنید اگر لازم باشد
// که JwtSettings را مستقیماً inject کنید (نه IOptions<JwtSettings>)،
// اما برای JwtTokenGenerator که IOptions<JwtSettings> می‌گیرد، Configure کافی است.
// builder.Services.AddSingleton(jwtSettings); // این خط معمولاً لازم نیست اگر از Configure استفاده می‌کنید
// و JwtTokenGenerator از IOptions<JwtSettings> استفاده می‌کند.

builder.Services.AddSingleton(Options.Create(jwtSettings)); // یا builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

// 2. ثبت IJwtTokenGenerator
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
// 3. پیکربندی Identity
builder.Services.AddIdentity<User, Domain.Entities.Users.Role>(options => // User و Role کلاس‌های شما هستند
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 8;
	options.User.RequireUniqueEmail = true;
	// سایر تنظیمات Identity
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // برای عملیات Reset Password, Email Confirmation و ...

// 4. پیکربندی Authentication با JWT Bearer
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings.Issuer,
		ValidAudience = jwtSettings.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
		ClockSkew = TimeSpan.Zero // عدم تحمل تاخیر در زمان انقضا
	};
});




var app = builder.Build();
app.UseMiddleware<MiddlewareExceptionHandler>();
app.UseStaticFiles();
await app.AddWebApplicationCollection();  //.ConfigureAwait(false)

app.Run();




//singlton => هر درخواست یکبار متد سازنده ساخته میشود
//transient => به ازای هر درخواست هر بار متد سازنده صدا زده میشود
//scoped  =>  به ازای هر درخواست یکبار متد سازنده ساخته میشود.
