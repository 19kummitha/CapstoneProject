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
        public GetAllResidentsHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthService");
        }
        public async Task<GetAllResidentsResponse> Handle(GetAllResidentsQuery request, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your-jwt-token");
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
