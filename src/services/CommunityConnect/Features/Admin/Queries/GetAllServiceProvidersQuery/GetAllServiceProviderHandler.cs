using CommunityConnect.DTO;
using MediatR;
using System.Net.Http.Headers;

namespace CommunityConnect.Features.Admin.Queries.GetAllServiceProvidersQuery
{
    public class GetAllServiceProviderQuery : IRequest<GetAllServiceProviderResponse>
    {
    }
    public class GetAllServiceProviderResponse
    {
        public IEnumerable<GetServiceDto> Service { get; set; }

    }
    public class GetAllServiceProviderHandler : IRequestHandler<GetAllServiceProviderQuery, GetAllServiceProviderResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetAllServiceProviderHandler(IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<GetAllServiceProviderResponse> Handle(GetAllServiceProviderQuery request, CancellationToken cancellationToken)
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                throw new HttpRequestException("Authorization header is missing");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeader.Replace("Bearear", ""));
            
            var response = await _httpClient.GetAsync("api/auth/service-provider", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var services = await response.Content.ReadFromJsonAsync<IEnumerable<GetServiceDto>>(cancellationToken: cancellationToken);
                return new GetAllServiceProviderResponse { Service = services };
            }

            // Handle error cases
            throw new HttpRequestException($"Error fetching serviceProviders: {response.StatusCode}");
        }
    }
}
