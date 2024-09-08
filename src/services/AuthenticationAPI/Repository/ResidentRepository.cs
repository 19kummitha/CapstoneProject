using AuthenticationAPI.Contracts;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly UserManager<Register> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ResidentRepository(UserManager<Register> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> DeleteResident(string id)
        {
            var resident=await _userManager.FindByIdAsync(id); 
            if(resident == null)
            {
                return false;
            }
            var result=await _userManager.DeleteAsync(resident);
            return true;
        }

        public async Task<IEnumerable<GetResidentDto>> GetAllResidents()
        {

            var residentRole = await _roleManager.FindByNameAsync("User");
            if (residentRole == null)
            {
                return null;
            }

            var residents = await _userManager.GetUsersInRoleAsync(residentRole.Name);

            var residentsDto = residents.Select(c => new GetResidentDto
            {
                Id = c.Id,
                UserName = c.UserName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                FlatNo=c.FlatNo,
                Name=c.Name

            }).ToList();
            return residentsDto;
        }

    }
}
