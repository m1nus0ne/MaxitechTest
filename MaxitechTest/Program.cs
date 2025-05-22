using System.Text;

namespace MaxitechTest;

public static class Program
{
    private static char[] Vowels { get; } = ['a', 'e', 'i', 'o', 'u', 'y'];

    public static void Main(string[] args)
    {
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            return;

        var invalidChars = input.Where(ch => ch is < 'a' or > 'z').ToArray();

        if (invalidChars.Length != 0)
        {
            Console.WriteLine("Введены неподходящие символы: " + new string(invalidChars));
        }
        else
        {
            var result = GetReversed(input);
            Console.WriteLine(result);

            var stats = CountCharacters(result);
            foreach (var stat in stats)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value}");
            };
            Console.WriteLine(MaxVowelsSubstring(result));
        }
    }

    private static string GetReversed(string input)
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
    private static Dictionary<char, int> CountCharacters(string input)
    {
        var counts = new Dictionary<char, int>();
        foreach (var ch in input)
        {
            if (!counts.TryAdd(ch, 1))
                counts[ch]++;
        }
        return counts;
    }

    private static string MaxVowelsSubstring(string input)
    {
        var startIndex = input.IndexOfAny(Vowels);
        var endIndex = input.LastIndexOfAny(Vowels);
        return input.Substring(startIndex, endIndex - startIndex+1);
    }
}