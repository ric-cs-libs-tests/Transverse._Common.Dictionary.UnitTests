using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Moq;


namespace Transverse._Common.Dictionary.UnitTests;

[TestClass]
public class DictionaryExtensionTests
{
    private readonly Dictionary<string, int> dictionary;

    public DictionaryExtensionTests()
    {
        dictionary = new()
        {
            {"aa", 10 },
            {"Aa", 100 },
            {"Bb", 200 },
            {"C", 300 },
        };

    }


    [TestMethod]
    public void ToKeyValueString_DefaultBehavior_ShouldReturnTheRightString()
    {
        var result = dictionary.ToKeyValueString<int>();
        var expected = $"aa={dictionary["aa"]};Aa={dictionary["Aa"]};Bb={dictionary["Bb"]};C={dictionary["C"]}";

        expected.Should().Be(result);
    }

    [TestMethod]
    [DataRow("/", "", "->")]
    [DataRow("*", "'", ":")]
    public void ToKeyValueString_WithParams_ShouldReturnTheRightString(string keyValueSeparator, string quoteValueSymbol, string keyValueEqualitySymbol)
    {
        var result = dictionary.ToKeyValueString<int>(keyValueSeparator, quoteValueSymbol, keyValueEqualitySymbol);
        var expected = string.Join(keyValueSeparator, (new string[] {
            $"aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["aa"]}{quoteValueSymbol}",
            $"Aa{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Aa"]}{quoteValueSymbol}",
            $"Bb{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["Bb"]}{quoteValueSymbol}",
            $"C{keyValueEqualitySymbol}{quoteValueSymbol}{dictionary["C"]}{quoteValueSymbol}",
        }));

        expected.Should().Be(result);
    }
}
