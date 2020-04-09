using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrimitiveExtensions
{
    /// <summary>
    /// Round <paramref name="value"/> to <paramref name="decimalPlaces"/> decimal places.
    /// </summary>
    /// <param name="value">float value</param>
    /// <param name="decimalPlaces">decimal places</param>
    /// <returns></returns>
    public static float RoundDecimalPlaces(this float value, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        return Mathf.Round(value * multiplier) / multiplier;
    }
}
