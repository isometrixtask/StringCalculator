using StringCalculatorApp;

namespace StringCalculatorAppTests;

public class StringCalculatorTests
{
    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var calculator = new StringCalculator();

        var numbers = string.Empty;
        var expected = 0;

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }
}