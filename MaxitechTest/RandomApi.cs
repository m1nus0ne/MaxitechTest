using System.Net.Http;
using System.Text.Json;

namespace MaxitechTest;

public class RandomApi(string apiUrl = "http://www.randomnumberapi.com/api/v1.0/random")
{
    private readonly HttpClient _httpClient = new();

    public async Task<int> GetRandomNumber(int maxExclusive)
    {
        try
        {
            var url = $"{apiUrl}?min=0&max={maxExclusive - 1}&count=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(jsonResponse);

            if (numbers is not null && numbers.Length > 0)
                return numbers[0];
        }
        catch (Exception)
        {
            return GetRandomNumberFallback(maxExclusive);
        }

        return GetRandomNumberFallback(maxExclusive);
    }

    private int GetRandomNumberFallback(int maxExclusive)
    {
        return Random.Shared.Next(maxExclusive);
    }
}