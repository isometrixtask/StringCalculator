namespace StringCalculatorApp;

public class StringCalculator
{
    public int Add(string numbersString)
    {
        var noNumbers = string.IsNullOrEmpty(numbersString);
        if (noNumbers) return 0;

        var delimiters = new[] { ",", Environment.NewLine };
        var numbers = NumbersFromDelimitedString(numbersString, delimiters);

        var result = numbers.Sum();

        return result;
    }

    private IEnumerable<int> NumbersFromDelimitedString(string numbersString, string[] delimiters)
    {
        var splitStrings = numbersString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var numbers = splitStrings.Select(int.Parse);

        return numbers;
    }
}