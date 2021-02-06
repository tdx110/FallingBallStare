using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UiLvl : MonoBehaviour
{
    #region Zmienne Inspektor i Klasa
    [SerializeField]
    [Header("Obiekt Główny Gra")]
    private GameObject gameObjectGame;
    [SerializeField]
    [Header("Obiekt z reklamami")]
    private Reklama adsUnityScript;
    [SerializeField]
    [Header("Obiekty Canvas")]
    private GameObject CanvasGameOver;
    [SerializeField]
    private GameObject CanvasLevelComplete;
    [SerializeField]
    [Header("Przyciski")]
    private GameObject nextLevelButton;
    [SerializeField]
    private GameObject gameObjectPause;
    [SerializeField]
    [Header("Napis na zakończenie poziomu")]
    private Text levelCompleteText;
    [SerializeField]
    [Header("Pole tekstowe z ilością monet")]
    private Text moneyText;

    //Czy pokazać przycisk następnego poziomu
    private bool showNextLevel;
    //Nazwa następnej sceny
    private string nextLevelSceneName;
    //Obiekt do sterowania. Domyślnie Kula
    private Rigidbody2D controllRigidbody;
    private float powerForce;
    private bool left, right;
    #endregion

    #region Funkcje
    #region Menu
    public void FunctionShowGameOver()
    {
        if (!CanvasLevelComplete.activeSelf)
        {
            Time.timeScale = 0;
            CanvasGameOver.SetActive(true);
        }
    }
    public void FunctionVisiableGamePause()
    {
        if (!CanvasGameOver.activeSelf && !CanvasLevelComplete.activeSelf)
        {
            if (gameObjectPause.activeSelf)
            {
                Time.timeScale = 1;
                gameObjectPause.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                gameObjectPause.SetActive(true);
            }
        }
    }
    public void FunctionLoadMainMenu()
    {
        adsUnityScript.ZwiększCountAds();
        SceneManager.LoadSceneAsync("MenuGłowne");
    }
    public void FunctionLoadNextLevel()
    {
        adsUnityScript.ZwiększCountAds();
        SceneManager.LoadSceneAsync(nextLevelSceneName);
    }
    public void FunctionRestart()
    {
        adsUnityScript.ZwiększCountAds();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void FunctionAddCoin(int howMuch)
    {
        if (ZmienneGlobalne.IlePieniędzy == 0)
        {
            ZmienneGlobalne.IlePieniędzy = 1;
        }
        else
        {
            ZmienneGlobalne.IlePieniędzy = ZmienneGlobalne.IlePieniędzy + howMuch;
        }
        moneyText.text = ZmienneGlobalne.IlePieniędzy.ToString();
        PlayerPrefs.SetInt(ZmienneGlobalne.PieniądzeString, ZmienneGlobalne.IlePieniędzy);
    }
    public void FunctionLoadCoin()
    {
        ZmienneGlobalne.IlePieniędzy = PlayerPrefs.GetInt(ZmienneGlobalne.PieniądzeString);
        moneyText.text = ZmienneGlobalne.IlePieniędzy.ToString();
    }
    public void FunctionLevelComplete(string numerNextLevel)
    {
        Time.timeScale = 0;
        PlayerPrefs.SetInt("Lvl " + numerNextLevel,1);
        CanvasLevelComplete.SetActive(true);
    }
    #endregion
    #region Rigidbody
    public void MoveLeft()
    {
        if (left) left = false;
        else left = true;
    }
    public void MoveRight()
    {
        if (right) right = false;
        else right = true;
    }
    #endregion
    #endregion

    private void Start()
    {
        left = false;
        right = false;
        Time.timeScale = 1;
        CanvasGameOver.SetActive(false);
        controllRigidbody = gameObjectGame.GetComponent<Gra>().ControllRigidbody2D;
        powerForce = gameObjectGame.GetComponent<Gra>().PowerForce;
        nextLevelSceneName = gameObjectGame.GetComponent<Gra>().NextLevelSceneName;
        levelCompleteText.text = gameObjectGame.GetComponent<Gra>().ThisLevelName;
        showNextLevel = gameObjectGame.GetComponent<Gra>().ShowNextLevel;
        if (showNextLevel) nextLevelButton.SetActive(true);
        else nextLevelButton.SetActive(false);
        FunctionLoadCoin();

    }
    private void Update()
    {
        if (right) controllRigidbody.AddForce(Vector2.right * Time.deltaTime * powerForce);
        if (left) controllRigidbody.AddForce(Vector2.left * Time.deltaTime * powerForce);
    }
}
