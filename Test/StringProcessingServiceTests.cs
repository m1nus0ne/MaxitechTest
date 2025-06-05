using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApi.Services;

namespace Test;

[TestFixture]
public class StringProcessingServiceTests
{
    private StringProcessingService _service;
    private Mock<IRandomApiService> _mockRandomApi;
    private Mock<IConfiguration> _mockConfiguration;
    private Mock<IConfigurationSection> _mockSection;

    [SetUp]
    public void Setup()
    {
        _mockRandomApi = new Mock<IRandomApiService>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockSection = new Mock<IConfigurationSection>();

        var blacklist = new[] { "forbidden", "blacklisted" };
        var mockChildren = blacklist.Select(word =>
        {
            var mockChildSection = new Mock<IConfigurationSection>();
            mockChildSection.Setup(s => s.Value).Returns(word);
            return mockChildSection.Object;
        }).ToList();

        _mockSection.Setup(s => s.GetChildren()).Returns(mockChildren);
        _mockConfiguration.Setup(c => c.GetSection("BlackList")).Returns(_mockSection.Object);

        _service = new StringProcessingService(_mockRandomApi.Object, _mockConfiguration.Object);
    }

    [TestCase("abcdef", "cbafed", TestName = "GetReversed_EvenLengthString")]
    [TestCase("hello", "ollehhello", TestName = "GetReversed_OddLengthString")]
    public void GetReversed_ShouldReturnCorrectResult(string input, string expected)
    {
        var result = _service.GetReversed(input);
        result.Should().Be(expected);
    }

    [TestCase("hello", new[] {"h:1", "e:1", "l:2", "o:1"}, TestName = "CountCharacters_Hello")]
    [TestCase("banana", new[] {"b:1", "a:3", "n:2"}, TestName = "CountCharacters_Banana")]
    public void CountCharacters_ShouldReturnCorrectCounts(string input, string[] expectedPairs)
    {
        Dictionary<char, int> expected = expectedPairs
            .Select(pair => pair.Split(':'))
            .ToDictionary(parts => parts[0][0], parts => int.Parse(parts[1]));
        
        var result = _service.CountCharacters(input);
        result.Should().BeEquivalentTo(expected);
    }

    [TestCase("hello world", "ello wo", TestName = "MaxVowelsSubstring_WithVowels")]
    [TestCase("zxcvd", "", TestName = "MaxVowelsSubstring_NoVowels")]
    [TestCase("a", "a", TestName = "MaxVowelsSubstring_SingleVowel")]
    public void MaxVowelsSubstring_ShouldReturnCorrectResult(string input, string expected)
    {
        var result = _service.MaxVowelsSubstring(input);
        result.Should().Be(expected);
    }

    [TestCase("aaa", "aaa", TestName = "TreeSort_MixedChars")]
    [TestCase("hello", "ehllo", TestName = "TreeSort_MixedChars")]
    [TestCase("", "", TestName = "QuickSort_EmptyString")]
    public void QuickSort_ShouldSortCharactersInString(string input, string expected)
    {
        var result = _service.QuickSort(input);
        result.Should().Be(expected);
    }

    [TestCase("hello", "ehllo", TestName = "TreeSort_MixedChars")]
    [TestCase("aaa", "aaa", TestName = "TreeSort_SameChars")]
    [TestCase("", "", TestName = "TreeSort_EmptyString")]
    public void TreeSort_ShouldSortCharactersInString(string input, string expected)
    {
        var result = _service.TreeSort(input);
        result.Should().Be(expected);
    }

    [TestCase("abcxyz", true, TestName = "ValidateInputChars_AllValidChars")]
    [TestCase("ab123", false, "123", TestName = "ValidateInputChars_SomeInvalidChars")]
    [TestCase("AB", false, "AB", TestName = "ValidateInputChars_UppercaseInvalidChars")]
    [TestCase("йцу", false, "йцу", TestName = "ValidateInputChars_СyrillicInvalidChars")]
    public void ValidateInputChars_ShouldCheckCharValidity(string input, bool expectedResult,
        string invalidCharsStr = "")
    {
        bool result = _service.ValidateInputChars(input, out var invalidChars);
        result.Should().Be(expectedResult);

        if (string.IsNullOrEmpty(invalidCharsStr))
        {
            invalidChars.Should().BeEmpty();
        }
        else
        {
            var expectedInvalidChars = invalidCharsStr.ToCharArray();
            invalidChars.Should().BeEquivalentTo(expectedInvalidChars);
        }
    }

    [TestCase("valid", true, TestName = "ValidateInputWords_NonBlacklistedWord")]
    [TestCase("forbidden", false, TestName = "ValidateInputWords_BlacklistedWord_Forbidden")]
    [TestCase("blacklisted", false, TestName = "ValidateInputWords_BlacklistedWord_Blacklisted")]
    public void ValidateInputWords_ShouldCheckBlacklist(string input, bool expectedResult)
    {
        bool result = _service.ValidateInputWords(input);
        result.Should().Be(expectedResult);
    }
}