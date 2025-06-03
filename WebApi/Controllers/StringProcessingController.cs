using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StringProcessingController(IStringProcessingService stringService) : ControllerBase
{
    [HttpGet("process")]
    public async Task<IActionResult> ProcessString([FromQuery] ProcessStringRequest request)
    {
        if (string.IsNullOrEmpty(request.Input))
            return BadRequest("Входная строка не может быть пустой");

        if (!stringService.ValidateInputChars(request.Input, out var invalidChars))
        {
            return BadRequest("Введены неподходящие символы: " + new string(invalidChars));
        }
        if (!stringService.ValidateInputWords(request.Input))
        {
            return BadRequest("Данное слово содержится в черном списке.");
        }

        var reversedString = stringService.GetReversed(request.Input);
        var characterStats = stringService.CountCharacters(reversedString);
        var vowelsSubstring = stringService.MaxVowelsSubstring(reversedString);

        string sortedString = request.SortMethod switch
        {
            SortingEnum.QuickSort => stringService.QuickSort(reversedString),
            SortingEnum.TreeSort => stringService.TreeSort(reversedString),
            _ => "Неверный метод сортировки."
        };

        var trimmedString = await stringService.RemoveRandomCharacter(reversedString);

        var result = new ProcessStringResponse
        {
            ReversedString = reversedString,
            CharacterStats = characterStats,
            VowelsSubstring = vowelsSubstring,
            SortedString = sortedString,
            TrimmedString = trimmedString
        };

        return Ok(result);
    }
}