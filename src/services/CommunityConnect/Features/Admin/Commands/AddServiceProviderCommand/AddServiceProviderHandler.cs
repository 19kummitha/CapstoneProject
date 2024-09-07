using CommunityConnect.DTO;
using MediatR;
using System.Text.Json;
using System.Text;
using Mapster;

namespace CommunityConnect.Features.Admin.Commands.AddServiceProviderCommand
{
    public class AddServiceProviderCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ServiceType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddServiceProviderHandler : IRequestHandler<AddServiceProviderCommand, bool>
    {
        private readonly HttpClient _httpClient;

        public AddServiceProviderHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
        }

        public async Task<bool> Handle(AddServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.Adapt<RegisterServiceProviderDto>();
            var content = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register-service", content, cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}
