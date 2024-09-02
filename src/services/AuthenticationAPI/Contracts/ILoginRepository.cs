using AuthenticationAPI.Models;

namespace AuthenticationAPI.Contracts
{
    public interface ILoginRepository
    {
        public Task<IResult> Login(Login model);
    }
}
