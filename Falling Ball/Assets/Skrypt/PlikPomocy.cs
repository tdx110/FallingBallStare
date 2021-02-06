using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlikPomocy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
 Opisy znaczenia kodów
0x0 - ustawianie granic ekranu. Ustawia niewidoczne BoxColidery, które będą stanowić granice ekranu.
    w przypadku bocznych granic stanowią one ścianę przez ktorą kulka nie będzie mogła przelatywać
    a w przypadku granicy górnej i dolnej to dotknięcie jej kończy grę.
0x1 - współczynnik Ratio. Przydatna dla programisty informacja o tym ile pixeli przypada na jedna jednostkę w Unity
0x2 - Spradza czy przycisk wcześniej istniał. Polega to na sprawdzeniu czy Gra wcześniej uruchamiana posiadała
    dostęp do tej planszy i czy umieściła w PlayerPrefs informacje o tym.
    jeśli nie to zostanie stworzony odpowiedni plik PlayerPrefs z domyślnym stanem (czy aktywny przycisk jest czy nie)
 */