using System;
using Unity.VisualScripting;
using UnityEngine;

public class TextFormatter
{   
    public string ReturnText(notationType currentNotation, double value)
    {
        switch (currentNotation) 
        {
            case notationType.normal:
                return value.ToString();

            case notationType.shortend:
                return FormatNumber(value);

            case notationType.scientific:
                return value.ToString("0.00E0");

            default:
                return "";
        }
    }
    public static string FormatNumber(double num)
    {
        var Epoint = Math.Log(num);

        if (num >= 1e33) return (num / 1e33).ToString("0.#") + "Dc";
        if (num >= 1e30) return (num / 1e30).ToString("0.#") + "No";
        if (num >= 1e27) return (num / 1e27).ToString("0.#") + "Oc";
        if (num >= 1e24) return (num / 1e24).ToString("0.#") + "Sp";
        if (num >= 1e21) return (num / 1e21).ToString("0.#") + "Sx";
        if (num >= 1e18) return (num / 1e18).ToString("0.#") + "Qi";
        if (num >= 1e15) return (num / 1e15).ToString("0.#") + "Qa";
        if (num >= 1e12) return (num / 1e12).ToString("0.#") + "T";
        if (num >= 1e9) return (num / 1e9).ToString("0.#") + "B";
        if (num >= 1e6) return (num / 1e6).ToString("0.#") + "M";
        if (num >= 1e3) return (num / 1e3).ToString("0.#") + "K";

        return num.ToString("0");
    }
}
