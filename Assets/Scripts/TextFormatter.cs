using UnityEngine;

public class TextFormatter
{
    public enum notationType { normal, shortend, scientific }
    public notationType currentNotation = notationType.shortend;

    public string ReturnText(int value)
    {
        switch (currentNotation) 
        {
            case notationType.normal:
                return value.ToString();

            case notationType.shortend:
                return FormatNumber(value);

            case notationType.scientific:
                return value.ToString("E0");

            default:
                return "";
        }
    }
    public static string FormatNumber(double num)
    {
        if (num >= 1_000_000_000_000_000)
            return (num / 1_000_000_000_000_000d).ToString("0.#") + "Qa";

        if (num >= 1_000_000_000_000)
            return (num / 1_000_000_000_000d).ToString("0.#") + "T";

        if (num >= 1_000_000_000)
            return (num / 1_000_000_000d).ToString("0.#") + "B";

        if (num >= 1_000_000)
            return (num / 1_000_000d).ToString("0.#") + "M";

        if (num >= 1_000)
            return (num / 1_000d).ToString("0.#") + "K";

        return num.ToString("0");
    }
}
