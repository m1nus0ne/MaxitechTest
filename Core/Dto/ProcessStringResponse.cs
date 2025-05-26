namespace Core.Dto;

public class ProcessStringResponse
{
    public string ReversedString { get; set; } = string.Empty;
    public Dictionary<char, int> CharacterStats { get; set; } = new();
    public string VowelsSubstring { get; set; } = string.Empty;
    public string SortedString { get; set; } = string.Empty;
    public string TrimmedString { get; set; } = string.Empty;
}