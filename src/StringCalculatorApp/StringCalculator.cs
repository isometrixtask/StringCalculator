namespace StringCalculatorApp;

public class StringCalculator
{
    public int Add(string numbers)
    {
        var noNumbers = string.IsNullOrEmpty(numbers);
        if (noNumbers) return 0;

        var result = int.Parse(numbers);

        return result;
    }
}