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

// �� ��� ������ ������ �� �������
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ����� �������� ������� �� appsetings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// �������� �� asp.net core
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ����� �� ��� ����� � ������ ���� ������ ��������  �� ���� �������� ���� IdentityOptionsProvider � ���� � ������� ����� �����
builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ������ �������� ������ � JWT ����� �� �� � ���� ���� ����� ��� �����
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


// ���������� ��������������
builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

// ���������� ������������
builder.Services.AddControllersWithViews();

//���������� razor pages
builder.Services.AddRazorPages();

// ����  �� ������ ������� ����� ����������� 
builder.Services.AddTransient<ITownService, TownService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IPropertyInspectionService, PropertyInspectionService>();
builder.Services.AddTransient<IDistrictService, DistrictService>();
builder.Services.AddTransient<IPropertyTypeService, PropertyTypeService>();
// ��� �� ������ ����
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
    // ���������� ���� ������
    AutoMapperConfiguration.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;
    // ����� �������� ��������� "ApplicationDbContext"
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // �������� ����������
    dbContext.Database.Migrate();
    // ������ ���� � ���� ����������� ����� �� �������� �� asp.net core
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // ������ ������� � ������
    ApplicationDbInitialiser.SeedRoles(roleManager);
    ApplicationDbInitialiser.SeedUsers(userManager);
}
// ���� �� ��������
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

// �������� ����
app.Run();
