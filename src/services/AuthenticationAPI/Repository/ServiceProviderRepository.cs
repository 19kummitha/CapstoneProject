using AuthenticationAPI.Contracts;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Repository
{
    public class ServiceProviderRepository:IServiceProviderRepository
    {
        private readonly UserManager<Register> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ServiceProviderRepository(UserManager<Register> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<GetServiceProviderDto>> GetAllServices()
        {

            var serviceRole = await _roleManager.FindByNameAsync("ServiceProvider");
            if (serviceRole == null)
            {
                return null;
            }

            var serviceProviders = await _userManager.GetUsersInRoleAsync(serviceRole.Name);

            var serviceDto = serviceProviders.Select(c => new GetServiceProviderDto
            {
                Id = c.Id,
                UserName = c.UserName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                ServiceType = c.ServiceType,
                Name = c.Name

            }).ToList();
            return serviceDto;
        }


        public async Task<bool> DeleteSeviceProvider(string id)
        {
            var serviceProvider = await _userManager.FindByIdAsync(id);
            if (serviceProvider == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(serviceProvider);
            return true;
        }
    }
}
