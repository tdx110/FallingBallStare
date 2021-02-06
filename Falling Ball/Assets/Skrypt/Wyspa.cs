using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyspa : MonoBehaviour
{
    #region Zmienne do Inspektora i wyspy
    public string NapisZwińRozwiń = "Rozwiń opcje dodatkowe";
    public float Prędkość;
    public int BierzącaPozycja = 0;


    //Czy wyspa może się poruszać
    public bool CzyPoruszać = false;
    //Czy wyspa ma się cofać czy zaczynać od pierwszej współrzędnej
    public bool CzyCofać = false;
    //Lista pozycji wyspy
    public Vector3[] ListaWspółrzędnych = new Vector3[0];
    //Lista z jaką prędkością się ma poruszać
    public float[] ListaPrędkosci = new float[0];

    #endregion

    #region Funkcje dla Inspektora
    /// <summary>
    /// Dodaje bierzącą pozycję z daną prędkością
    /// </summary>
    /// <param name="prędkość">Prędkość przemieszczania się</param>
    public void DodajPozycjeMiny(float prędkość)
    {
        Array.Resize(ref ListaWspółrzędnych, ListaWspółrzędnych.Length + 1);
        Array.Resize(ref ListaPrędkosci, ListaPrędkosci.Length + 1);
        ListaWspółrzędnych[ListaWspółrzędnych.Length - 1] = gameObject.transform.localPosition;
        ListaPrędkosci[ListaPrędkosci.Length - 1] = prędkość;
        BierzącaPozycja = ListaWspółrzędnych.Length - 1;
    }
    /// <summary>
    /// Edytuje współrzędne bierzącej pozycji na bierzącą lokalizację
    /// </summary>
    public void EdytujPozycję()
    {
        if (ListaWspółrzędnych.Length > 0)
        {
            ListaWspółrzędnych[BierzącaPozycja] = gameObject.transform.localPosition;
            ListaPrędkosci[BierzącaPozycja] = Prędkość;
        }
    }
    /// <summary>
    /// Usuwa ostatnią pozycję z tablicy
    /// </summary>
    public void UsuńOstatniąPozycję()
    {
        if (ListaWspółrzędnych.Length > 0)
        {
            Array.Resize(ref ListaWspółrzędnych, ListaWspółrzędnych.Length - 1);
            Array.Resize(ref ListaPrędkosci, ListaPrędkosci.Length - 1);
        }

    }
    /// <summary>
    /// Usuwa wszystkie pozycje i prędkości
    /// </summary>
    public void UsuńWszystkiePozycje()
    {
        ListaWspółrzędnych = new Vector3[0];
        ListaPrędkosci = new float[0];
    }
    /// <summary>
    /// Funkcja przesuwa Wyspę na poprzednią pozycję.
    /// </summary>
    public void NastępnaPozycja()
    {
        if (BierzącaPozycja < ListaWspółrzędnych.Length - 1)
        {
            BierzącaPozycja = BierzącaPozycja + 1;
        }
        else
        {
            BierzącaPozycja = 0;
        }
        if (ListaWspółrzędnych.Length != 0)
        {
            gameObject.transform.localPosition = ListaWspółrzędnych[BierzącaPozycja];
            Prędkość = ListaPrędkosci[BierzącaPozycja];
        }
    }
    /// <summary>
    /// Funkcja przesuwa Wyspę na poprzednią pozycję.
    /// </summary>
    public void PoprzedniaPozycja()
    {
        if (BierzącaPozycja > 0)
        {
            BierzącaPozycja = BierzącaPozycja - 1;
        }
        else
        {
            if (ListaWspółrzędnych.Length != 0)
            {
                BierzącaPozycja = ListaWspółrzędnych.Length - 1;

            }
        }
        if (ListaWspółrzędnych.Length != 0)
        {
            gameObject.transform.localPosition = ListaWspółrzędnych[BierzącaPozycja];
            Prędkość = ListaPrędkosci[BierzącaPozycja];
        }
    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
