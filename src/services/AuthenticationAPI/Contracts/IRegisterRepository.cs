using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Contracts
{
    public interface IRegisterRepository
    {
        public Task<ActionResult> Register(Register model, params string[] roles);
    }
}
