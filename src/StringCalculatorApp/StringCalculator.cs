﻿using System.Text.RegularExpressions;

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
            var customDelimiters = ExtractCustomDelimiters(numbersString);
            delimiters.AddRange(customDelimiters);
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

    private IEnumerable<string> ExtractCustomDelimiters(string calculationString)
    {
        var delimitersStartIndex = CustomDelimiterPrefix.Length;
        var delimitersEndIndex = calculationString.IndexOf(CustomDelimiterSuffix, StringComparison.Ordinal);
        var delimiterLength = delimitersEndIndex - delimitersStartIndex;

        var delimiterDefinitions = calculationString.Substring(delimitersStartIndex, delimiterLength);

        var customDelimiters = new List<string>();

        var isEnclosedInBrackets = delimiterDefinitions.StartsWith("[") && delimiterDefinitions.EndsWith("]");
        if (isEnclosedInBrackets)
        {
            var delimiterPattern = @"\[(.+?)\]";
            var matches = Regex.Matches(delimiterDefinitions, delimiterPattern);
            foreach (Match match in matches)
            {
                customDelimiters.Add(match.Groups[1].Value);
            }
        }
        else
        {
            customDelimiters.Add(delimiterDefinitions);
        }

        return customDelimiters;
    }

    private IEnumerable<int> NumbersFromDelimitedString(string numbersString, string[] delimiters)
    {
        var splitStrings = numbersString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var numbers = splitStrings.Select(int.Parse);

        return numbers;
    }
}