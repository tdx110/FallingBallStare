using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obiekty : MonoBehaviour
{
    #region Zmienne
    //Czy Miny mog¹ siê poruszaæ
    public bool MoveMiny = false;
    //Czy wyspy mog¹ siê poruszaæ
    public bool MoveWyspy = false;
    //Nazwa obiektu z elementami na planszy
    public GameObject WordLevel;
    public RectTransform ResolutionCanvas;
    #endregion

    private void Start()
    {
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector2(ResolutionCanvas.localScale.x, ResolutionCanvas.localScale.y);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(ResolutionCanvas.sizeDelta.x, ResolutionCanvas.sizeDelta.y);
    }
    
}
