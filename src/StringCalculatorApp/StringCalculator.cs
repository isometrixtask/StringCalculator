namespace StringCalculatorApp;

public class StringCalculator
{
    private const string CustomDelimiterPrefix = "//";
    private static readonly string CustomDelimiterSuffix = Environment.NewLine;
    private static readonly string[] DefaultDelimiters = [",", Environment.NewLine];
    private const int MaximumNumber = 1000;

    public int Add(string numbersString)
    {
        var noNumbers = string.IsNullOrEmpty(numbersString);
        if (noNumbers) return 0;

        var delimiters = DefaultDelimiters.ToList();

        var customDelimitersPresent = numbersString.StartsWith(CustomDelimiterPrefix);
        if (customDelimitersPresent)
        {
            var customDelimiter = ExtractCustomDelimiter(numbersString);
            delimiters.Add(customDelimiter);
            numbersString = numbersString.Split(CustomDelimiterSuffix, 2).Last();
        }

        var numbers = NumbersFromDelimitedString(numbersString, delimiters.ToArray());

        CheckForNegativeNumbers(numbers);
        numbers = ExcludeNumbersGreaterThanMaximum(numbers);

        var result = numbers.Sum();

        return result;
    }

    private void CheckForNegativeNumbers(IEnumerable<int> numbers)
    {
        const string negativeNumbersMessage = "Negatives not allowed";
        var negativeNumbers = numbers.Where(number => number < 0);
        var negativeNumbersPresent = negativeNumbers.Any();
        if (negativeNumbersPresent)
        {
            throw new ArgumentException($"{negativeNumbersMessage}: {string.Join(", ", negativeNumbers)}");
        }
    }

    private IEnumerable<int> ExcludeNumbersGreaterThanMaximum(IEnumerable<int> numbers)
    {
        return numbers.Where(number => number <= MaximumNumber);
    }

    private string ExtractCustomDelimiter(string calculationString)
    {
        var delimitersStartIndex = CustomDelimiterPrefix.Length;
        var delimitersEndIndex = calculationString.IndexOf(CustomDelimiterSuffix, StringComparison.Ordinal);
        var delimiterLength = delimitersEndIndex - delimitersStartIndex;

        var customDelimiter = calculationString.Substring(delimitersStartIndex, delimiterLength);

        var isEnclosedInBrackets = customDelimiter.StartsWith("[") && customDelimiter.EndsWith("]");
        if (isEnclosedInBrackets)
        {
            customDelimiter = customDelimiter.Trim(['[', ']']);
        }

        return customDelimiter;
    }

    private IEnumerable<int> NumbersFromDelimitedString(string numbersString, string[] delimiters)
    {
        var splitStrings = numbersString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var numbers = splitStrings.Select(int.Parse);

        return numbers;
    }
}