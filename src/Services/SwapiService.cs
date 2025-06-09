using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class SwapiService
{
    private readonly HttpClient _httpClient;

    public SwapiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Planet> GetPlanetAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<SwapiResponse<Planet>>($"https://www.swapi.tech/api/planets/{id}/");
        return response?.Result?.Properties;
    }

        public async Task<Planet> GetPeopleAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<SwapiResponse<Planet>>($"https://www.swapi.tech/api/people/{id}/");
        return response?.Result?.Properties;
    }
}

public class SwapiResponse<T>
{
    public string Message { get; set; }
    public SwapiResult<T> Result { get; set; }
}

public class SwapiResult<T>
{
    public T Properties { get; set; }
}
