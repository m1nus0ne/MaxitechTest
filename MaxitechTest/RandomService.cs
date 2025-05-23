using System.Net.Http;
using System.Text.Json;

namespace MaxitechTest;

public class RandomService
{
    private readonly HttpClient _httpClient = new();

    public RandomService()
    {
        _httpClient.BaseAddress = new Uri("http://www.randomnumberapi.com/api/v1.0"); //TODO: move to .env
        
    }

    public int GetRandomNumber(int maxExclusive)
    {
        return GetRandomNumberAsync(maxExclusive).Result;
    }

    public async Task<int> GetRandomNumberAsync(int maxExclusive)
    {
        try
        {
            var url = $"/random?min=0&max={maxExclusive - 1}&count=1";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(jsonResponse) ?? [];
            if (numbers.Length > 0)
                return numbers[0];
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибка при запросе случайного числа");
        }
        return GetRandomNumberFallback(maxExclusive);
    }
    
    private int GetRandomNumberFallback(int maxExclusive)
    {
        return Random.Shared.Next(maxExclusive);
    }
}