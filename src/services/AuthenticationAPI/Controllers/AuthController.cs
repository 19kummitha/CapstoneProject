using AuthenticationAPI.Contracts;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IRegisterRepository _registerRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IServiceProviderRepository _serviceProviderRepository;

        public AuthController(ILoginRepository loginRepository, IRegisterRepository registerRepository, IResidentRepository residentRepository, IServiceProviderRepository serviceProviderRepository)
        {
            _loginRepository = loginRepository;
            _registerRepository = registerRepository;
            _residentRepository = residentRepository;
            _serviceProviderRepository = serviceProviderRepository;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("Hey")]
        public IActionResult Get()
        {
            return Ok("You are logged in");
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            return await _registerRepository.Register(model);

        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            return await _registerRepository.RegisterAdmin(model);

        }
        [HttpPost]
        [Route("register-service")]
        public async Task<IActionResult> RegisterService([FromBody] Register model)
        {
            return await _registerRepository.RegisterService(model);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var login = await _loginRepository.Login(model);

            return Ok(login);
        }
        [HttpGet]
        [Route("resident")]
        public async Task<IActionResult> GetAllResidents()
        {
            var residents = await _residentRepository.GetAllResidents();
            return Ok(residents);
        }
        [HttpGet]
        [Route("service-provider")]
        public async Task<IActionResult> GetAllServiceProviders()
        {
            var serviceProvider = await _serviceProviderRepository.GetAllServices();
            return Ok(serviceProvider);
        }
        [HttpDelete]
        [Route("delete-resident/{id}")]
        public async Task<IActionResult> DeleteResident([FromRoute] string id)
        {
            var result=await _residentRepository.DeleteResident(id);
            if(result)
            {
                return Ok(new { Message = "Resident Deleted Successfully" });
            }
            return NotFound(new { Message = "Resident Not Found" });
        }
        [HttpDelete]
        [Route("delete-ServiceProvider/{id}")]
        public async Task<IActionResult> DeleteSeviceProvider([FromRoute] string id)
        {
            var result = await _serviceProviderRepository.DeleteSeviceProvider(id);
            if (result)
            {
                return Ok(new { Message = "ServiceProvider Deleted Successfully" });
            }
            return NotFound(new { Message = "ServiceProvider Not Found" });
        }
    }
}
