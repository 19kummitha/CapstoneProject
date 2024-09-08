using MediatR;
using System.Net.Http.Headers;

namespace CommunityConnect.Features.Admin.Commands.DeleteResidentCommand
{
    public class DeleteResidentCommand : IRequest<bool>
    {
        public string ResidentId { get; set; }
    }
    public class DeleteResidentCommandHandler : IRequestHandler<DeleteResidentCommand, bool>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteResidentCommandHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(DeleteResidentCommand request, CancellationToken cancellationToken)
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                throw new HttpRequestException("Authorization header is missing");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeader.Replace("Bearer", ""));

            // Send DELETE request to authentication API
            var response = await _httpClient.DeleteAsync($"api/auth/delete-resident/{request.ResidentId}", cancellationToken);

            // Check response
            return response.IsSuccessStatusCode;
        }
    }
}
