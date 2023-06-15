namespace Terminal.Helpers;
public static class ConsoleHelper
{
    public static void WriteColorLine(string text, ConsoleColor color)
    {
        ConsoleColor tempColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = tempColor;
    }
}
