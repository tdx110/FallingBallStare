using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    [SerializeField]
    [Header("UI Start")]
    private GameObject CanvasStart;
    [SerializeField]
    [Header("US Nowa Gra")]
    private GameObject CanvasNewGame;
    // Start is called before the first frame update
    private void Awake()
    {
        #region Sprawdzanie i Ustawianie PlayerPrefs
        //Sprawdza i ustawia licznik do uruchomienia reklam
        // Jeśli nie było o nim informacji w PlayerPrefs
        if (!PlayerPrefs.HasKey(ZmienneGlobalne.CountAdsString))
        {
            PlayerPrefs.SetInt(ZmienneGlobalne.CountAdsString, 0);
        }
        else
        {

        }

        #endregion
    }
    void Start()
    {
        Time.timeScale = 1;
    }

    //Naciśnięcie przycisku New Game
    public void VisiableNewGame()
    {
        if (CanvasStart.activeSelf)
        {
            CanvasStart.SetActive(false);
            CanvasNewGame.SetActive(true);
        }
        else
        {
            CanvasStart.SetActive(true);
            CanvasNewGame.SetActive(false);
        }
    }

    //
    public void Exit()
    {
        Application.Quit();
    }
}

