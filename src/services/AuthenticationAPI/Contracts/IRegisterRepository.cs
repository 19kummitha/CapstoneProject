using AuthenticationAPI.Models;

namespace AuthenticationAPI.Contracts
{
    public interface IRegisterRepository
    {
        public Task<IResult> Register(Register model, params string[] roles);
    }
}
