using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PoziomPrzycisk : MonoBehaviour
{
    #region Zmienne do obsługi przycisku
    //Informacja czy przycisk jest aktywny
    [SerializeField]
    [Header("Czy poziom jest aktywny")]
    private bool aktywny;
    //Nazwa/numer poziomu wyświetlany
    [SerializeField]
    [Header("Numer poziomu wyświetlany")]
    private string numer;
    [SerializeField]
    [Header("Obiekt scena do wczytania")]
    private string sceneLvl;
    //Nazwa główna przy zapisywaniu PlayerPrefs
    private string nazwa = "Lvl ";
    #endregion

    #region Obiekty do obsługi przycisku
    //Główny przycisk do obsługi wczytania planszy
    [SerializeField]
    [Header("Przycisk w Prefab")]
    private GameObject przyciskGłówny;
    //Pole tekstowe wyświetlające numer/nazwę planszy
    [SerializeField]
    [Header("Pole tekstowe z numerem/nazwą planszy")]
    private GameObject poleTekstowe;
    #endregion

    private void OnEnable()
    {
        //Debug.Log("Pobiera przycisk. " + przyciskGłówny.name);
        Button button = przyciskGłówny.GetComponent<Button>();
        //Debug.Log("Sprawdza czy wcześniej przycisk istniał - Kod 0x2");
        if (PlayerPrefs.HasKey(nazwa + numer) == false)
        {
            if (aktywny) PlayerPrefs.SetInt(nazwa + numer, 1);
            else PlayerPrefs.SetInt(nazwa + numer, 0);
        }
        //Debug.Log("Pobiera pole tekstowe i ustawia numer/nazwę przycisku planszy");
        Text text = poleTekstowe.GetComponent<Text>();
        text.text = numer.ToString();
        //Debug.Log("Sprawdza stan poziomu w PlayerPrefs. Czy aktywny, czy nie.");
        if (PlayerPrefs.GetInt(nazwa + numer, 2) == 0) button.interactable = false;
        else if (PlayerPrefs.GetInt(nazwa + numer, 2) == 1) button.interactable = true;
        //else Debug.LogError("Wczytanie wartości nie powiodło się");
    }
    private void OnDisable()
    {
        //Debug.Log("Zapisuje ostatni stan w jakim był przycisk przed jego zamknięciem.");
        Button button = przyciskGłówny.GetComponent<Button>();
        if (button.interactable) PlayerPrefs.SetInt(nazwa + numer,1);
        else PlayerPrefs.SetInt(nazwa + numer, 0);
    }
    public void WczytajPlansze()
    {
        //Debug.Log("Wczytuje odpowiednią plansze");
        SceneManager.LoadSceneAsync(sceneLvl, LoadSceneMode.Single);
    }
}
