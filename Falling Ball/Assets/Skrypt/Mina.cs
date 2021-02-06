using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mina : MonoBehaviour
{
    #region Zmienne dla inspektora
    [NonSerialized]
    public string NapisZwińRozwiń = "Rozwiń opcje dodatkowe";
    [NonSerialized]
    public float Prędkość;
    [NonSerialized]
    public int BierzącaPozycja = 0;

    #endregion
    #region Zmienne w inspektorze
    //[SerializeField]
    //[Header("Obiekt z kamerą główną do pobrania danych", order = 0)]
    //[Header("czy wszystkie miny mogą się poruszać", order = 1)]
    public Obiekty ObiektyIPrzeszkody;
    //[SerializeField]
    //[Header("Czy mina ma się poruszać?")]
    public bool czyPoruszać;
    //[SerializeField]
    //[Header("Współrzędne do jakich ma się przemieścić Mina")]
    public Vector3[] listaWspółrzędnych = new Vector3[0];
    //[SerializeField]
    //[Header("Prędkość przemieszczania się.")]
    public float[] prędkośćMiny = new float[0];
    //[SerializeField]
    //[Header("Po zakończeniu ma powrócić na swoją pozycję?")]
    public bool czyPowrócić;
    #endregion

    #region Funkcje w Inspektorze dla programisty
    /// <summary>
    /// Dodaje bierzącą pozycję z daną prędkością
    /// </summary>
    /// <param name="prędkość">Prędkość przemieszczania się</param>
    public void DodajPozycjeMiny(float prędkość)
    {
        Array.Resize(ref listaWspółrzędnych, listaWspółrzędnych.Length + 1);
        Array.Resize(ref prędkośćMiny, prędkośćMiny.Length + 1);
        listaWspółrzędnych[listaWspółrzędnych.Length - 1] = gameObject.transform.localPosition;
        prędkośćMiny[prędkośćMiny.Length - 1] = prędkość;
        BierzącaPozycja = listaWspółrzędnych.Length - 1;
    }
    /// <summary>
    /// Edytuje współrzędne bierzącej pozycji na bierzącą lokalizację
    /// </summary>
    public void EdytujPozycję()
    {
        if (listaWspółrzędnych.Length > 0)
        {
            listaWspółrzędnych[BierzącaPozycja] = gameObject.transform.localPosition;
            prędkośćMiny[BierzącaPozycja] = Prędkość;
        }
    }
    /// <summary>
    /// Usuwa ostatnią pozycję z tablicy
    /// </summary>
    public void UsuńOstatniąPozycję()
    {
        if (listaWspółrzędnych.Length > 0)
        {
            Array.Resize(ref listaWspółrzędnych, listaWspółrzędnych.Length - 1);
            Array.Resize(ref prędkośćMiny, prędkośćMiny.Length - 1);
        }

    }
    /// <summary>
    /// Usuwa wszystkie pozycje i prędkości
    /// </summary>
    public void UsuńWszystkiePozycje()
    {
        listaWspółrzędnych = new Vector3[0];
        prędkośćMiny = new float[0];
    }
    /// <summary>
    /// Funkcja przesuwa Minę na poprzednią pozycję.
    /// </summary>
    public void NastępnaPozycja()
    {
        if (BierzącaPozycja < listaWspółrzędnych.Length - 1)
        {
            BierzącaPozycja = BierzącaPozycja + 1;
        }
        else
        {
            BierzącaPozycja = 0;
        }
        if (listaWspółrzędnych.Length != 0)
        {
            gameObject.transform.localPosition = listaWspółrzędnych[BierzącaPozycja];
            Prędkość = prędkośćMiny[BierzącaPozycja];
        }
    }
    /// <summary>
    /// Funkcja przesuwa Minę na poprzednią pozycję.
    /// </summary>
    public void PoprzedniaPozycja()
    {
        if (BierzącaPozycja > 0)
        {
            BierzącaPozycja = BierzącaPozycja - 1;
        }
        else
        {
            if (listaWspółrzędnych.Length != 0)
            {
                BierzącaPozycja = listaWspółrzędnych.Length - 1;

            }
        }
        if (listaWspółrzędnych.Length != 0)
        {
            gameObject.transform.localPosition = listaWspółrzędnych[BierzącaPozycja];
            Prędkość = prędkośćMiny[BierzącaPozycja];
        }
    }

    #endregion

    //Zapisuje prędkość w zależności od Time.deltaTime
    private float dystansDoPrzebycia;
    //Zmienna przechowująca bierzący etap poruszanie się
    private int krok;
    //Czy ma liczyć do pozycji początkowej
    private bool czyCofać;
    //Obiekty Miny potrzebny do obliczeń wewnątrz klasy
    private Vector3 gameObjectMinaPozycja;
    // Start is called before the first frame update

    void Start()
    {
        //Ustawia wykonywany krok na początkowy
        krok = 0;
        //Ustawia że normalnie ma iść. Nie ma potrzeby jeszcze cofania kroków
        czyCofać = false;
    }

    private void FixedUpdate()
    {
        //Sprawdza czy obiekt został dołączony
        //Jeśli nie to nie ma obsługi przemieszczania Min
        if (listaWspółrzędnych.Length != 0)
        {
            //Sprawdza czy konkretna mina może się poruszać
            if (czyPoruszać)
            {
                //Pobiera lokalizację lokalkną miny względem rodzica
                gameObjectMinaPozycja = this.gameObject.transform.localPosition;
                //W zaelżności od tego w krórą stronę liczy kroki, ustawia odpowiednią prędkość.
                //Potrzebne aby w obie strony z tą samą prędkością poruszała się mina
                #region Ustawianie dystansDoPrzebycia (prędkości) Miny
                if (czyCofać)
                {
                    if (krok == listaWspółrzędnych.Length - 1)
                    {
                        dystansDoPrzebycia = Time.deltaTime * prędkośćMiny[krok];
                        //Debug.Log(prędkośćMiny[krok]);
                    }
                    else
                    {
                        dystansDoPrzebycia = Time.deltaTime * prędkośćMiny[krok + 1];
                        //Debug.Log(prędkośćMiny[krok+1]);
                    }
                }
                else
                {
                    if (krok == 0)
                    {
                        dystansDoPrzebycia = Time.deltaTime * prędkośćMiny[krok + 1];
                        //Debug.Log(prędkośćMiny[krok+1]);
                    }
                    else
                    {
                        dystansDoPrzebycia = Time.deltaTime * prędkośćMiny[krok];
                        //Debug.Log(prędkośćMiny[krok]);
                    }
                }
                #endregion
                //Oblicza czy pozostały dystans jaki musi pokonać jest większy od dystansDoPrzebycia
                //Czy mniejszy. Jeśli mniejszy po prostu przepisuje lokalizację
                #region Zmiana Położenia Miny
                //Oś X
                if (Math.Abs(listaWspółrzędnych[krok].x - gameObjectMinaPozycja.x) > dystansDoPrzebycia)
                {
                    //Jeśli jest większy to w którą stronę ma się poruszać obiekt
                    if (listaWspółrzędnych[krok].x - gameObjectMinaPozycja.x > 0)
                    {
                        gameObjectMinaPozycja.x = gameObjectMinaPozycja.x + dystansDoPrzebycia;
                    }
                    else
                    {
                        gameObjectMinaPozycja.x = gameObjectMinaPozycja.x - dystansDoPrzebycia;
                    }
                }
                else
                {
                    //Jeśli jest mniejszy to ma się przemieścić do punktu
                    gameObjectMinaPozycja.x = listaWspółrzędnych[krok].x;
                }

                //Oś Y
                if (Math.Abs(listaWspółrzędnych[krok].y - gameObjectMinaPozycja.y) > dystansDoPrzebycia)
                {
                    //Jeśli jest większy to w którą stronę ma się poruszać obiekt
                    if (listaWspółrzędnych[krok].y - gameObjectMinaPozycja.y > 0)
                    {
                        gameObjectMinaPozycja.y = gameObjectMinaPozycja.y + dystansDoPrzebycia;
                    }
                    else
                    {
                        gameObjectMinaPozycja.y = gameObjectMinaPozycja.y - dystansDoPrzebycia;
                    }
                }
                else
                {
                    //Jeśli jest mniejszy to ma się przemieścić do punktu
                    gameObjectMinaPozycja.y = listaWspółrzędnych[krok].y;
                }
                #endregion

                //Wykonuje jeśli obiekt jest już w danej pozycji
                if (gameObjectMinaPozycja == listaWspółrzędnych[krok])
                {
                    //Spradza czy po osiągnięciu ostatniego kroku Mina ma wracać do początku
                    //Jeśli tak to cofa od końca do początku
                    if (czyPowrócić)
                    {
                        //Sprawdza w którym kierunku kieruje się Mina
                        //Czy na początek (czyCofać == true)
                        //Czy jeszcze do ostatniego punktu (czyCofać == false)
                        if (czyCofać)
                        {
                            //Jeśli osiągnął już początek to wyłącza cofanie
                            // else - jeśli nie doszła do pierwszego kroku
                            // to stopniowo zmniejsza kroki
                            if (krok == 0)
                            {
                                czyCofać = false;
                            }
                            else
                            {
                                krok = krok - 1;
                            }
                        }
                        else
                        {
                            //Jeśli Mina osiągnęła ostatni punkt to włacza cofanie
                            //else - jeśli nie doszła do ostatniego punktu
                            // to zwiększa krok na następny
                            if (krok == listaWspółrzędnych.Length - 1)
                            {
                                czyCofać = true;
                            }
                            else
                            {
                                krok = krok + 1;
                            }
                        }
                    }
                    else
                    {
                        //Jeśli nie ma się cofać
                        //To po osiągnięciu ostatniego kroku
                        //automatycznie kieruje się do pierwszego
                        if (krok < listaWspółrzędnych.Length - 1)
                        {
                            krok = krok + 1;
                        }
                        else
                        {
                            krok = 0;
                        }
                    }
                }
                //Przepisuje zmienną tymczasową z nowym położeniem do obiektu
                // (aktualizuje położenie Miny)
                this.gameObject.transform.localPosition = gameObjectMinaPozycja;
            }
        }
    }
}
