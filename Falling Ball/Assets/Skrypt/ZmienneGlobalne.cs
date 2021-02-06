
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZmienneGlobalne
{
    #region Opcje Debugowania
    public static bool CzyDebugowanie = true;
    #endregion
    #region Zmienne Globalne
    //Wymiary ekranu (w jednostkach Unity)
    /// <summary>
    /// Szerokosć ekranu
    /// </summary>
    public static float SzerokośćEkranu;
    /// <summary>
    /// Wysokość ekranu
    /// </summary>
    public static float WysokośćEkranu;
    /// <summary>
    /// Ratio Ekranu
    /// </summary>
    public static float RatioEkranu;
    /// <summary>
    /// Co ile ma być wyświetlana reklama
    /// Liczy od "0", więc więcej o jeden
    /// </summary>
    public static int MaxCountAds = 3;
    /// <summary>
    /// Przechowuje informacje ile mamy pieniędzy
    /// </summary>
    public static int IlePieniędzy;
    #endregion

    #region Gdzie są zapisywane dane
    /// <summary>
    /// Gdzie przechowywana jest ostatnia wersja gry w PlayerPrefs
    /// </summary>
    public static string VersjaString = "Version";
    /// <summary>
    /// Gdzie przechowywane są pieniądze w PlayerPrefs
    /// </summary>
    public static string PieniądzeString = "Pieniądze";
    /// <summary>
    /// Gdzie zapisywana jest liczba wczytanych i restartowanych plansz w PlayerPrefs
    /// </summary>
    public static string CountAdsString = "Count Ads";
    #endregion
}
