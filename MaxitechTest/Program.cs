using System.Text;

namespace MaxitechTest;

public static class Program
{
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
}