using System.Text.Json;

namespace WebApi.Services;

public class RandomApiService(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
    : IRandomApiService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("RandomApi");

    private readonly string apiUrl = configuration.GetValue<string>("RandomApi:BaseUrl") ??
                                      "https://www.randomnumberapi.com/api/v1.0/random";

    public async Task<int> GetRandomNumber(int maxExclusive)
    {
        if (maxExclusive <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxExclusive),
                "Максимальное значение должно быть положительным");
        }

        try
        {
            var url = $"{apiUrl}?min=0&max={maxExclusive - 1}&count=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(jsonResponse);
            if (numbers is not null && numbers.Length > 0)
            {
                return numbers[0];
            }
            return GetRandomNumberFallback(maxExclusive);
        }
        catch (Exception ex)
        {
            return GetRandomNumberFallback(maxExclusive);
        }
    }

    private static int GetRandomNumberFallback(int maxExclusive)
    {
        return Random.Shared.Next(maxExclusive);
    }
}