using CommunityConnect.DTO;
using Mapster;
using MediatR;
using System.Text.Json;
using System.Text;

namespace CommunityConnect.Features.Admin.Commands.AddResidentCommand
{
    public class AddResidentCommand: IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FlatNo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddResidentHandler : IRequestHandler<AddResidentCommand, bool>
    {
        private readonly HttpClient _httpClient;

        public AddResidentHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
        }

        public async Task<bool> Handle(AddResidentCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.Adapt<RegisterResidentDto>();
            var content = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register", content, cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}
