﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedHome.Identity;
using SharedHome.Identity.Entities;

namespace SharedHome.Infrastructure.EF.Initializers.Identity
{
    public class IdentityInitializer : IDataInitializer
    {
        private readonly InitializerOptions _initializerOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(IOptions<InitializerOptions> initializerOptions, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _initializerOptions = initializerOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddRoles();

            await AddDefaultUsersWithRoles();
        }

        private async Task AddRoles()
        {
            if (!await _roleManager.RoleExistsAsync(AppIdentityConstants.Roles.Administrator))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = InitializerConstants.AdministratorRoleId,
                    Name = AppIdentityConstants.Roles.Administrator,
                    NormalizedName = AppIdentityConstants.Roles.Administrator.ToUpper(),
                });
            }


            if (!await _roleManager.RoleExistsAsync(AppIdentityConstants.Roles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = InitializerConstants.UserRoleId,
                    Name = AppIdentityConstants.Roles.User,
                    NormalizedName = AppIdentityConstants.Roles.User.ToUpper(),
                });
            }
        }

        private async Task AddDefaultUsersWithRoles()
        {
            if(await _userManager.FindByIdAsync(InitializerConstants.AdminUserId.ToString()) is null)
            {
                var administrator = new ApplicationUser
                {
                    Id = InitializerConstants.AdminUserId.ToString(),
                    FirstName = InitializerConstants.AdminFirstName,
                    LastName = InitializerConstants.AdminLastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Email = InitializerConstants.AdminEmail,
                    NormalizedEmail = InitializerConstants.AdminEmail.ToUpper(),
                    UserName = InitializerConstants.AdminUserName,
                    NormalizedUserName = InitializerConstants.AdminUserName.ToUpper(),
                };

                await _userManager.CreateAsync(administrator, _initializerOptions.AdminPassword);

                await _userManager.AddToRoleAsync(administrator, AppIdentityConstants.Roles.Administrator);
            }

            if (await _userManager.FindByIdAsync(InitializerConstants.CharlesUserId.ToString()) is null)
            {
                var charles = new ApplicationUser
                {
                    Id = InitializerConstants.CharlesUserId.ToString(),
                    FirstName = InitializerConstants.CharlesFirstName,
                    LastName = InitializerConstants.CharlesLastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Email = InitializerConstants.CharlesEmail,
                    NormalizedEmail = InitializerConstants.CharlesEmail.ToUpper(),
                    UserName = InitializerConstants.CharlesUserName,
                    NormalizedUserName = InitializerConstants.CharlesUserName.ToUpper(),
                };

                await _userManager.CreateAsync(charles, _initializerOptions.CharlesPassword);

                await _userManager.AddToRoleAsync(charles, AppIdentityConstants.Roles.Administrator);
            }

            if (await _userManager.FindByIdAsync(InitializerConstants.FrancUserId.ToString()) is null)
            {
                var franc = new ApplicationUser
                {
                    Id = InitializerConstants.FrancUserId.ToString(),
                    FirstName = InitializerConstants.FrancFirstName,
                    LastName = InitializerConstants.FrancLastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Email = InitializerConstants.FrancEmail,
                    NormalizedEmail = InitializerConstants.FrancEmail.ToUpper(),
                    UserName = InitializerConstants.FrancUserName,
                    NormalizedUserName = InitializerConstants.FrancUserName.ToUpper(),
                };

                await _userManager.CreateAsync(franc, _initializerOptions.FrancPassword);

                await _userManager.AddToRoleAsync(franc, AppIdentityConstants.Roles.Administrator);
            }
        }
    }
}
