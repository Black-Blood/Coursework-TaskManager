namespace PL;

internal static class WindowManager
{
    internal delegate bool ValidateMethod(string inputData);
    internal delegate void MenuMethod();

    private static readonly int _windowWidth = 75;

    internal static void Clear()
    {
        Console.Clear();
    }


    internal static void ShowTitle(string title)
    {
        Clear();
        Console.WriteLine("".PadLeft(_windowWidth, '-'));
        Console.WriteLine("|" + title.PadLeft((_windowWidth - 2) / 2 + title.Length / 2, ' ').PadRight(_windowWidth - 2, ' ') + "|");
        Console.WriteLine("".PadLeft(_windowWidth, '-'));
    }

    internal static void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        ShowLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static void ShowMenu(string menuName, string[] menuItems)
    {
        ShowLine(menuName + ": ");

        for (byte i = 0; i < menuItems.Length; i++)
            ShowLine("   " + i + " - " + menuItems[i]);
    }


    internal static string GetData(string message, string helperText, ValidateMethod? validator = null)
    {
        string? inputData;

        for (byte i = 0; i < 6; i++)
        {
            if (i > 0)
            {
                ShowMessage("Invalid data! Your attems left - " + (6 - i), ConsoleColor.Yellow);
                ShowMessage(helperText);
                ShowMessage("");
            }


            Console.Write("| " + message + ": ");
            inputData = Console.ReadLine();


            if (inputData == null)
                throw new Exception("Result is null. Why?");


            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ShowLine(message + ": " + inputData);
            if (validator == null || validator(inputData))
                return inputData;
        }

        throw new Exception("Too much fails!");
    }

    internal static void SelectMenu(KeyValuePair<string, MenuMethod>[] menuAndMethod)
    {
        string[] menu = Array.ConvertAll(menuAndMethod, (menuItem) => menuItem.Key);

        ShowMenu("Choose", menu);

        string menuIndex = GetData(
            message: "Type number",
            validator: (inputData) => int.TryParse(inputData, out int result) && result < menu.Length,
            helperText: ""
        );

        menuAndMethod[int.Parse(menuIndex)].Value();
    }


    private static void ShowLine(string line)
    {
        Console.WriteLine("| " + line.PadRight(_windowWidth - 4, ' ') + " |");
    }
}

