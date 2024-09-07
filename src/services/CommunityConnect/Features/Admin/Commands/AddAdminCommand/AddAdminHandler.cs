using CommunityConnect.DTO;
using Mapster;
using MediatR;
using System.Text.Json;
using System.Text;

namespace CommunityConnect.Features.Admin.Commands.AddAdminCommand
{
    public class AddAdminCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddAdminHandler : IRequestHandler<AddAdminCommand, bool>
    {
        private readonly HttpClient _httpClient;

        public AddAdminHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
        }

        public async Task<bool> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.Adapt<RegisterAdminDto>();
            var content = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register-admin", content, cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}
