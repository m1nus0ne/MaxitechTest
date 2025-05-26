using System.Text.Json.Serialization;

namespace Core.Dto;

public class ProcessStringRequest
{
    public string Input { get; set; }
    public SortingEnum SortMethod { get; set; }
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortingEnum
{
    QuickSort,
    TreeSort
}