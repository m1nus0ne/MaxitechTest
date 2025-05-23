using System.Text;

namespace MaxitechTest;

public static class Program
{
    private static readonly char[] vowels = ['a', 'e', 'i', 'o', 'u'];

    public static void Main(string[] args)
    {
        var input = Console.ReadLine();
        if (!IsValidInput(input))
            return;

        var reversedString = GetReversed(input!);
        Console.WriteLine(reversedString);

        var stats = CountCharacters(reversedString);
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }

        Console.WriteLine(MaxVowelsSubstring(reversedString));

        Console.WriteLine("Сортировать с помощью быстрой сортировки(1) или деревом(2)?");
        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine(SortingAlgorithms.QuickSort(reversedString));
                break;
            case "2":
                Console.WriteLine(SortingAlgorithms.TreeSort(reversedString));
                break;
            default:
                Console.WriteLine("Неправильный ввод");
                break;
                
        }
        
    }

    private static bool IsValidInput(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        var invalidChars = input.Where(ch => ch is < 'a' or > 'z').ToArray();
        if (invalidChars.Length != 0)
        {
            Console.WriteLine("Введены неподходящие символы: " + new string(invalidChars));
            return false;
        }

        return true;
    }

    private static string GetReversed(string input)
    {
        var chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    private static Dictionary<char, int> CountCharacters(string input)
    {
        return input
            .GroupBy(c => c)
            .Select(c => new { c.Key, Value = c.Count() })
            .ToDictionary(x => x.Key, x => x.Value);
    }
    private static string RemoveRandomCharacter(string input)
    {
        var randomApi = new RandomApi();
        var index =  randomApi.GetRandomNumber(input.Length).GetAwaiter().GetResult();
        return input.Remove(index, 1);
    }

    private static string MaxVowelsSubstring(string input)
    {
        var startIndex = input.IndexOfAny(vowels);
        if (startIndex == -1)
            return string.Empty;
        var endIndex = input.LastIndexOfAny(vowels);
        return input.Substring(startIndex, endIndex - startIndex + 1);
    }
}