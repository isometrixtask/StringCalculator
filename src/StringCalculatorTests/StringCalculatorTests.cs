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

    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    [InlineData("3", 3)]
    public void Add_SingleNumber_ReturnsNumber(string numbers, int expected)
    {
        var calculator = new StringCalculator();

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,2", 3)]
    [InlineData("3,2", 5)]
    [InlineData("4,4", 8)]
    public void Add_TwoNumbers_ReturnsSum(string numbers, int expected)
    {
        var calculator = new StringCalculator();

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,2,3", 6)]
    [InlineData("3,2,1,8", 14)]
    [InlineData("4,4,0,10,0,1", 19)]
    public void Add_AnyAmountOfNumbers_ReturnsSum(string numbers, int expected)
    {
        var calculator = new StringCalculator();

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_NumbersWithNewlineDelimiter_ReturnsSum()
    {
        var calculator = new StringCalculator();

        var numbers = $"1{Environment.NewLine}2,3";
        var expected = 6;

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_CustomDelimiter_ReturnsSum()
    {
        var calculator = new StringCalculator();

        var numbers = $"//;{Environment.NewLine}1;2";
        var expected = 3;

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,2,-3", "Negatives not allowed: -3")]
    [InlineData("3,-2,-1,8", "Negatives not allowed: -2, -1")]
    [InlineData("-4,-4,-10", "Negatives not allowed: -4, -4, -10")]
    public void Add_ContainsNegativeNumber_Throws(string numbers, string expected)
    {
        var calculator = new StringCalculator();

        var exception  = Assert.Throws<ArgumentException>(
            () => calculator.Add(numbers)
        );

        Assert.Equal(expected, exception.Message);
    }

    [Theory]
    [InlineData("2, 1001", 2)]
    [InlineData("2, 1001, 3", 5)]
    public void Add_NumbersGreaterThan1000_NotSummed(string numbers, int expected)
    {
        var calculator = new StringCalculator();

        var result = calculator.Add(numbers);
        Assert.Equal(expected, result);
    }
}