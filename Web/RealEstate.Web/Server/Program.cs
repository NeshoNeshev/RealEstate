using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models.ApplicationModels;
using RealEstate.Data.Seeding;
using System.IdentityModel.Tokens.Jwt;
using ApplicationDbContext = RealEstate.Data.ApplicationDbContext;
using RealEstate.Services.Mapping;
using RealEstate.Web.Shared;
using System.Reflection;
using RealEstate.Services;

// от тук тръгва всичко за сървъра
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// взима конекшън стринга от appsetings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// дефолтно за asp.net core
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// казва че има юзери и добавя роли обърни внимание  на този статичен клас IdentityOptionsProvider в него е описано какво прави
builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// добавя идентити сървър и JWT токън за да е ясно дали юзера има права
builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    const string OpenId = "openid";

                    options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Email);
                    options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Email);

                    options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Role);
                    options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Role);
                });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(JwtClaimTypes.Role);


// регистрира аутентикациите
builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

// регистрира контролерите
builder.Services.AddControllersWithViews();

//регистрира razor pages
builder.Services.AddRazorPages();

// тези  са нашите сървиси туксе регистрират 
builder.Services.AddTransient<ITownService, TownService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IPropertyInspectionService, PropertyInspectionService>();
builder.Services.AddTransient<IDistrictService, DistrictService>();
builder.Services.AddTransient<IPropertyTypeService, PropertyTypeService>();
// тук се билдва аппа
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope = app.Services.CreateScope())
{
    // регистрира ауто мапъра
    AutoMapperConfiguration.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;
    // взима датабаза контекста "ApplicationDbContext"
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // стартира миграциите
    dbContext.Database.Migrate();
    // взимат роля и юзер мениджърите които са дефолтни за asp.net core
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // сиидва юзерите и ролите
    ApplicationDbInitialiser.SeedRoles(roleManager);
    ApplicationDbInitialiser.SeedUsers(userManager);
}
// тези са дефолтни
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

// стартира аппа
app.Run();
