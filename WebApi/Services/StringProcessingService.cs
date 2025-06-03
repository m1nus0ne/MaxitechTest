using System.Text;
using MaxitechTest;

namespace WebApi.Services;

public class StringProcessingService(IRandomApiService randomApi, IConfiguration configuration)
    : IStringProcessingService
{
    private readonly HashSet<string> _blacklistedWords = new(
        configuration.GetSection("BlackList").Get<string[]>() ?? []);

    private static readonly char[] Vowels = ['a', 'e', 'i', 'o', 'u', 'y'];

    public string GetReversed(string input)
    {
        var n = input.Length;
        var result = new StringBuilder { Capacity = 2 * n };

        if (n % 2 == 0)
        {
            var half = n / 2;
            for (int i = half - 1; i >= 0; i--)
                result.Append(input[i]);
            for (int i = n - 1; i >= half; i--)
                result.Append(input[i]);
        }
        else
        {
            for (int i = n - 1; i >= 0; i--)
                result.Append(input[i]);
            result.Append(input);
        }

        return result.ToString();
    }

    public Dictionary<char, int> CountCharacters(string input)
    {
        var counts = new Dictionary<char, int>();
        foreach (var ch in input)
        {
            if (!counts.TryAdd(ch, 1))
                counts[ch]++;
        }

        return counts;
    }

    public string MaxVowelsSubstring(string input)
    {
        var startIndex = input.IndexOfAny(Vowels);
        if (startIndex == -1)
            return string.Empty;
        var endIndex = input.LastIndexOfAny(Vowels);
        return input.Substring(startIndex, endIndex - startIndex + 1);
    }

    public string QuickSort(string input)
    {
        return SortingAlgorithms.QuickSort(input);
    }

    public string TreeSort(string input)
    {
        return SortingAlgorithms.TreeSort(input);
    }

    public async Task<string> RemoveRandomCharacter(string input)
    {
        var index = await randomApi.GetRandomNumber(input.Length);
        return input.Remove(index, 1);
    }

    public bool ValidateInputChars(string input, out char[] invalidChars)
    {
        invalidChars = input.Where(ch => ch is < 'a' or > 'z').ToArray();
        return invalidChars.Length == 0;
    }

    public bool ValidateInputWords(string input)
    {
        return !_blacklistedWords.Contains(input);
    }
}