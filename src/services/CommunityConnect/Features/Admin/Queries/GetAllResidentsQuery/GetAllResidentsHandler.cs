using CommunityConnect.DTO;
using MediatR;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityConnect.Features.Admin.Queries.GetAllResidentsQuery
{
    public class GetAllResidentsQuery : IRequest<GetAllResidentsResponse>
    {
    }
    public class GetAllResidentsResponse
    {
        public IEnumerable<GetResidentDto> Residents { get; set; }

    }
    public class GetAllResidentsHandler : IRequestHandler<GetAllResidentsQuery, GetAllResidentsResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetAllResidentsHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetAllResidentsResponse> Handle(GetAllResidentsQuery request, CancellationToken cancellationToken)
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if(string.IsNullOrEmpty(authHeader))
            {
                throw new HttpRequestException("Authorization header is missing");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeader.Replace("Bearear", ""));

            
            var response = await _httpClient.GetAsync("api/auth/resident", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var residents = await response.Content.ReadFromJsonAsync<IEnumerable<GetResidentDto>>(cancellationToken: cancellationToken);
                return new GetAllResidentsResponse { Residents = residents };
            }

            // Handle error cases
            throw new HttpRequestException($"Error fetching residents: {response.StatusCode}");
        }
    }
}
