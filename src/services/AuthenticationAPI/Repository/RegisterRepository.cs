using AuthenticationAPI.Contracts;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAPI.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ActionResult> Register( Register model, params string[] roles)
        {
            
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.ServiceProvider))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ServiceProvider));


            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            foreach (var role in roles)
                await _userManager.AddToRoleAsync(user, role);

            return new OkObjectResult(new Response { Status = "Success", Message = "User created successfully!" });
        }

    }
}
