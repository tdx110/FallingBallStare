using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kamera : MonoBehaviour
{
    #region Zmienne dla Inspektora i klasy
    #region Inspektor
    //Czy ma wiêcej informacji wyœwietliæ o opisie kamery
    public bool KameraMoreInfo = false;
    #endregion
    #region Zmienne
    #region Zmienne Kamery
    //Co kamera ma robiæ
    public CoRobiæKamera TypeKamera = CoRobiæKamera.Nothing;
    //Lista punktów po którym bêdzie siê poruszaæ kamera
    public Vector3[] PointList = new Vector3[0];
    //Lista prêdkoœci kamery
    public float[] SpeedList = new float[0];
    //Element listy na jaki przemieszcza siê kamera
    public int stepMoving = 0;
    //Aktualna prêdkoœæ
    public float ActualSpeed = 0;
    //Numer aktualnego kroku
    public int StepsPointList = 0;
    //Pozycja Kuli od której zaczyna siê pod¹¿anie
    public float StartWorkPosition = 0;
    #endregion
    #region T³o
    //Tablice przechowuj¹ce sk³adowe koloru
    public float[] ColorRBackground = new float[0];
    public float[] ColorGBackground = new float[0];
    public float[] ColorBBackground = new float[0];
    public float[] ColorABackground = new float[0];
    #endregion
    //Czy ma ju¿ zacz¹æ wykonywaæ operacjê przemieszczania, albo pod¹¿ania
    public bool StartWork = false;
    //Jaka ró¿nica odleg³oœci mo¿e byæ miedzy kulk¹ a kamer¹
    //Jak bêdzie kamera pod¹¿aæ za nim
    public float DeltaDistance = 0.3f;
    #endregion
    #region Obiekty
    //Obiekt ob³ugi Kuli
    public GameObject Ball;
    //Image z T³em
    public Image imageBackground;
    #endregion
    #endregion
    #region Funkcje do Inspektora
    //Dodaje wspó³rzêdn¹ do listy punktów
    public void AddPoint()
    {
        //Zmiana rozmiaru tablicy
        Array.Resize(ref PointList, PointList.Length + 1);
        Array.Resize(ref SpeedList, PointList.Length + 1);
        Array.Resize(ref ColorRBackground, PointList.Length + 1);
        Array.Resize(ref ColorGBackground, PointList.Length + 1);
        Array.Resize(ref ColorBBackground, PointList.Length + 1);
        Array.Resize(ref ColorABackground, PointList.Length + 1);

        PointList[PointList.Length - 1] = this.gameObject.transform.position;
        SpeedList[PointList.Length - 1] = ActualSpeed;
        ColorRBackground[PointList.Length - 1] = imageBackground.color.r;
        ColorGBackground[PointList.Length - 1] = imageBackground.color.g;
        ColorBBackground[PointList.Length - 1] = imageBackground.color.b;
        ColorABackground[PointList.Length - 1] = imageBackground.color.a;
        ActualSpeed = 0;
        StepsPointList = StepsPointList + 1;
    }
    //Odejmuje wspó³rzêdn¹ z listy punktów
    public void RemoveLastPoint()
    {
        int length;
        if (PointList.Length != 0)
        {
            if (StepsPointList == PointList.Length)
            {
                StepsPointList = StepsPointList - 1;
            }
            if (StepsPointList != 0) ActualSpeed = SpeedList[StepsPointList - 1];
            else ActualSpeed = 0;
            length = PointList.Length - 1;
            Array.Resize(ref SpeedList, length);
            Array.Resize(ref PointList, length);
            Array.Resize(ref ColorRBackground, length);
            Array.Resize(ref ColorGBackground, length);
            Array.Resize(ref ColorBBackground, length);
            Array.Resize(ref ColorABackground, length);
        }
    }
    //Poprawia pozycjê punktu
    public void CorrectPoint()
    {
        if (StepsPointList !=0)
        {
            PointList[StepsPointList - 1] = this.gameObject.transform.position;
            SpeedList[StepsPointList - 1] = ActualSpeed;
            ColorRBackground[StepsPointList - 1] = imageBackground.color.r;
            ColorGBackground[StepsPointList - 1] = imageBackground.color.g;
            ColorBBackground[StepsPointList - 1] = imageBackground.color.b;
            ColorABackground[StepsPointList - 1] = imageBackground.color.a;
        }
    }
    //Czyœci listê punktów
    public void ErasePointList()
    {
        PointList = new Vector3[0];
        SpeedList = new float[0];
        ColorRBackground = new float[0];
        ColorGBackground = new float[0];
        ColorBBackground = new float[0];
        ColorABackground = new float[0];
        StepsPointList = 0;
    }
    //Zmiana pozycji na któr¹? True - wstecz, False - nastêpn¹
    /// <summary>
    /// Zmienia aktualny krok.
    /// </summary>
    /// <param name="reverse">True - Wstecz, False - Nastêpny</param>
    public void ChangeStep(bool reverse)
    {
        if (PointList.Length != 0)
        {
            int length;
            //wykonuje jeœli aktualna pozycja to 0
            if (reverse)
            {
                if (StepsPointList == 1) StepsPointList = PointList.Length;
                else StepsPointList = StepsPointList - 1;
            }
            else
            {
                if (StepsPointList == PointList.Length) StepsPointList = 1;
                else StepsPointList = StepsPointList + 1;
            }
            length = StepsPointList - 1;
            ActualSpeed = SpeedList[length];
            this.gameObject.transform.position = PointList[length];
            imageBackground.color = new Color(ColorRBackground[length],
                ColorGBackground[length], ColorBBackground[length], ColorABackground[length]);
        }
    }
    #endregion
    private void Update()
    {

        if ((Ball.transform.position.y - this.gameObject.transform.position.y) < StartWorkPosition)
        {
            StartWork = true;
        }
        if (StartWork)
        {
            Vector3 positionKamera = this.gameObject.transform.position;
            float distanceX = (float)Math.Abs(gameObject.transform.position.x - PointList[stepMoving].x);
            float distanceY = (float)Math.Abs(gameObject.transform.position.y - PointList[stepMoving].y);
            float speedSTEP = SpeedList[stepMoving] * Time.deltaTime;

            switch (TypeKamera)
            {
                case CoRobiæKamera.Nothing:
                    break;
                case CoRobiæKamera.PunktToPunkt:
                    #region Point To Point
                    #region Przemieszczanie kamery
                    if (distanceX == 0 && distanceY == 0)
                    {
                        if (stepMoving == PointList.Length - 1) StartWork = false;
                        else stepMoving = stepMoving + 1;
                    }
                    else
                    {
                        if (distanceX > speedSTEP)
                        {
                            if (PointList[stepMoving].x < positionKamera.x)
                                positionKamera = new Vector3(positionKamera.x - speedSTEP, positionKamera.y, positionKamera.z);
                            else
                                positionKamera = new Vector3(positionKamera.x + speedSTEP, positionKamera.y, positionKamera.z);
                        }
                        else positionKamera = new Vector3(PointList[stepMoving].x, positionKamera.y, positionKamera.z);
                        if (distanceY > speedSTEP)
                        {
                            if (PointList[stepMoving].y < positionKamera.y)
                                positionKamera = new Vector3(positionKamera.x, positionKamera.y - speedSTEP, positionKamera.z);
                            else
                                positionKamera = new Vector3(positionKamera.x, positionKamera.y - speedSTEP, positionKamera.z);
                        }
                        else positionKamera = new Vector3(positionKamera.x, PointList[stepMoving].y, positionKamera.z);
                        this.gameObject.transform.position = positionKamera;

                    }
                    #endregion
                    #region Zmiana koloru t³a
                    float vector2AllDistance, vectorDistanceCamera, proportion;
                    Color colorDifference, colorStart ;
                    if (stepMoving == 0)
                    {
                        imageBackground.color = new Color(ColorRBackground[stepMoving],
                            ColorGBackground[stepMoving], ColorBBackground[stepMoving], ColorABackground[stepMoving]);
                    }
                    else
                    {
                        vector2AllDistance = Vector2.Distance(new Vector2(PointList[stepMoving - 1].x, PointList[stepMoving - 1].y),
                            new Vector2(PointList[stepMoving].x, PointList[stepMoving].y));
                        vectorDistanceCamera = Vector2.Distance(new Vector2(PointList[stepMoving - 1].x, PointList[stepMoving - 1].y),
                            new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y));
                        colorStart = new Color(ColorRBackground[stepMoving - 1], ColorGBackground[stepMoving - 1],
                            ColorBBackground[stepMoving - 1], ColorABackground[stepMoving - 1]);
                        colorDifference = new Color(ColorRBackground[stepMoving] - colorStart.r,ColorGBackground[stepMoving] - colorStart.g,
                            ColorBBackground[stepMoving] - colorStart.b,ColorABackground[stepMoving] - colorStart.a);
                        proportion = vectorDistanceCamera/vector2AllDistance;

                        imageBackground.color = colorStart+ (colorDifference *proportion);
                    }

                    #endregion
                    #endregion
                    break;
                case CoRobiæKamera.FollowPlayer:
                    #region Follow Player
                    if (Math.Abs(PointList[stepMoving].x - this.gameObject.transform.position.x) > DeltaDistance)
                    {

                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
    public enum CoRobiæKamera
    {
        //Kamera bêdzie sta³a w miejscu i nic nie robi³a
        Nothing,
        //Kamera przemieszcza siê miedzy punktami
        PunktToPunkt,
        //Kamera pod¹¿a za Kul¹
        FollowPlayer
    }
}
