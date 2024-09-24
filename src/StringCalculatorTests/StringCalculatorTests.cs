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

    [Fact]
    public void Add_SingleNumber_ReturnsNumber()
    {
        var calculator = new StringCalculator();

        var numbers = "1";
        var expected = 1;

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }
}