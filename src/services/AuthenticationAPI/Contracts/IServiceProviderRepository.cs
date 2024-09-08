using AuthenticationAPI.DTO;

namespace AuthenticationAPI.Contracts
{
    public interface IServiceProviderRepository
    {
        public Task<IEnumerable<GetServiceProviderDto>> GetAllServices();
    }
}
