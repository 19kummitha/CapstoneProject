using AuthenticationAPI.Contracts;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAPI.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<Register> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterRepository(UserManager<Register> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ActionResult> Register( Register model)
        {   var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User already exists!" });

            Register user = new()
            {
                Email= model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name=model.Name,
                FlatNo=model.FlatNo,
                ServiceType="",
                PhoneNumber=model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.PasswordHash);
            if (!result.Succeeded)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return new OkObjectResult(new Response { Status = "Success", Message = "User created successfully!" });
        }
        public async Task<ActionResult> RegisterAdmin(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Admin is already registered!!" });

            Register user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name = model.Name,
                FlatNo = "",
                ServiceType="",
                PhoneNumber= model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.PasswordHash);
            if (!result.Succeeded)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Admin Registration failed!!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);



            return new OkObjectResult(new Response { Status = "Success", Message = "User Registration completed:)" });
        }
        public async Task<ActionResult> RegisterService(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User already exists!" });

            Register user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name = model.Name,
                FlatNo = "",
                ServiceType=model.ServiceType,
                PhoneNumber= model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.PasswordHash);
            if (!result.Succeeded)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.ServiceProvider))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ServiceProvider));

            if (await _roleManager.RoleExistsAsync(UserRoles.ServiceProvider))
                await _userManager.AddToRoleAsync(user, UserRoles.ServiceProvider);

            return new OkObjectResult(new Response { Status = "Success", Message = "Service Provider created successfully!" });
        }
    }
}
