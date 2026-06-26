using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
                                       RoleManager<IdentityRole> roleManager,
                                       ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        DisplayName = "Omar Mohamed",
                        UserName = "Omarmohamed",
                        Email = "OmarMohamed@gmail.com",
                        PhoneNumber = "01111111111"
                    };
                    var User02 = new ApplicationUser()
                    {
                        DisplayName = "Lilyan Mohamed",
                        UserName = "Lilyanmohamed",
                        Email = "LilyanMohamed@gmail.com",
                        PhoneNumber = "01111221111"
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0re");
                    await _userManager.CreateAsync(User02, "P@ssw0re");
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Seeding Identity Database : Message = {ex.Message}");
            }
        }
    }
}
