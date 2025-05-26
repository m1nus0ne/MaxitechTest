namespace WebApi.Services;

public interface IStringProcessingService
{
    string GetReversed(string input);
    Dictionary<char, int> CountCharacters(string input);
    string MaxVowelsSubstring(string input);
    string QuickSort(string input);
    string TreeSort(string input);
    Task<string> RemoveRandomCharacter(string input);
    bool ValidateInput(string input, out char[] invalidChars);
}