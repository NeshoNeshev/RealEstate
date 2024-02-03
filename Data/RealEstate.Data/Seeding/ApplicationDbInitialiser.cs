using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Common;
using RealEstate.Data.Models.ApplicationModels;
using RealEstate.Data.Models.DatabaseModels;

namespace RealEstate.Data.Seeding
{
    // вкарва предварителни данни в базата свурзани с  потребител и роля
    public static class ApplicationDbInitialiser
    {
        //вкарва ролите в базата
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            AddRoles(roleManager, GlobalConstants.AdministratorRoleName);
            AddRoles(roleManager, GlobalConstants.ModeratorRoleName);
        }
        // вкарва потребителите в базата и ги вкарва в роля
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            (string name, string password, string role)[] demoUsers = new[]
            {
                (name: GlobalConstants.AdministratorUserName, password: GlobalConstants.AdministratorPassword, role: GlobalConstants.AdministratorRoleName),
                (name: GlobalConstants.ModeratorUserName, password: GlobalConstants.ModeratorPassword, role: GlobalConstants.ModeratorRoleName),

            };

            foreach ((string name, string password, string role) user in demoUsers)
            {
                AddUsers(userManager, user);
            }

        }
        public static async Task SeedTowns(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }
            var towns = new List<string>()
            {
            "София", "Варна" , "Пловдив" , "Бургас" , "Благоевград" ,  "Велико Търново", "Видин", "Враца" , "Габрово" ,  "Добрич" ,  "Кърджали" ,  "Кюстендил" ,
             "Ловеч" , "Монтана" ,  "Пазарджик" ,  "Перник" ,  "Плевен" ,"Разград" , "Русе" , "Силистра", "Сливен",
            "Смолян" ,  "Стара Загора","Търговище", "Хасково","Шумен", "Ямбол"
            };
            var townsToadd = new List<Town>();
            foreach (var item in towns)
            {
                townsToadd.Add(new Town() {Id= Guid.NewGuid().ToString(), Name = item });
            }


            dbContext.Towns.AddRange(townsToadd);
            await dbContext.SaveChangesAsync();
        }
        public static async Task SeedTipes(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PropertyTypes.Any())
            {
                return;
            }
            var propType = new List<string>()
            {
            "Едностаен апартамент", "Двустаен апартамент" , "Тристаен апартамент" , "Мезонет" , "Къща" ,  "Магазин",
            };
            var types = new List<PropertyType>();
            foreach (var item in propType)
            {
                types.Add(new PropertyType() { Id = Guid.NewGuid().ToString(), Name = item });
            }


            dbContext.PropertyTypes.AddRange(types);
            await dbContext.SaveChangesAsync();
        }
        //скрит метод за потребителите 
        private static void AddUsers(UserManager<ApplicationUser> userManager, (string name, string password, string role) demoUser)
        {
            ApplicationUser user = userManager.FindByNameAsync(demoUser.name).Result;
            if (user == default)
            {
                var newAppUser = new ApplicationUser
                {
                    UserName = demoUser.name,
                    Email = demoUser.name,
                    EmailConfirmed = true
                };
                _ = userManager.CreateAsync(newAppUser, demoUser.password).Result;
                if (!string.IsNullOrWhiteSpace(demoUser.role))
                {
                    var roles = demoUser.role.Split(',').Select(a => a.Trim()).ToArray();
                    Console.WriteLine($"{roles.Length}");
                    foreach (var role in roles)
                    {
                        _ = userManager.AddToRoleAsync(newAppUser, role).Result;

                    }
                }

            }
        }
        //скрит метод за ролите
        private static void AddRoles(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleManager.FindByNameAsync(roleName).Result == default)
            {
                roleManager.CreateAsync(new IdentityRole { Name = roleName }).Wait();
            }
        }
    }
}
