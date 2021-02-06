using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mina))]
public class MinaInspector : Editor
{
    public override void OnInspectorGUI()
    {

        //DrawDefaultInspector();
        Mina mina = (Mina)target;

        //Pobiera listę wszystkich rzeczy w skrypcie "mina"
        SerializedObject serializedObjectGłówny = new SerializedObject(mina);

        GUILayout.Label("Obiekt w który są obiekty i przeszkody");
        GUILayout.Label("oraz ustawienia globalne, czy mogą się poruszać");
        mina.ObiektyIPrzeszkody = (Obiekty)EditorGUILayout.ObjectField("Object with script Objekt", mina.ObiektyIPrzeszkody, typeof(Obiekty), true);
        GUILayout.Space(10);
        GUILayout.Label("Czy mina ma zezwolenie na poruszanie się.");
        mina.czyPoruszać = EditorGUILayout.Toggle("Czy poruszać:", mina.czyPoruszać);
        if (mina.czyPoruszać)
        {
            GUILayout.Space(10);
            GUILayout.Label("Sposób poruszania się miny");
            mina.czyPowrócić = EditorGUILayout.Toggle("Czy wracać:", mina.czyPowrócić);
            GUILayout.Space(10);
            //Dodatkowe przyciski funkcyjne
            GUILayout.Space(10);
            if (mina.listaWspółrzędnych.Length == 0)
            {
                GUILayout.Label("Bierząca pozycja: 0");
            }
            else
            {
                GUILayout.Label("Bierząca pozycja: " + (mina.BierzącaPozycja + 1));
            }
            GUILayout.Label("Zapisanych pozycji: " + mina.listaWspółrzędnych.Length);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Dodaj współrzędną"))
            {
                mina.DodajPozycjeMiny(mina.Prędkość);
            }
            if (GUILayout.Button("Edytuj Współrzędną"))
            {
                mina.EdytujPozycję();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Usuń ostatnią pozycję"))
            {
                mina.UsuńOstatniąPozycję();
            }
            if (GUILayout.Button("Usuń wszystkie współrzędne"))
            {
                mina.UsuńWszystkiePozycje();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Następna pozycja"))
            {
                mina.NastępnaPozycja();
            }
            if (GUILayout.Button("Poprzednia pozycja"))
            {
                mina.PoprzedniaPozycja();
            }
            GUILayout.EndHorizontal();
            mina.Prędkość = EditorGUILayout.FloatField("Prędkość przejazdu:", mina.Prędkość);
            GUILayout.Space(10);

            #region Tworzenie listyWspółrzędnych
            //WAŻNE!!!
            //Tworzy listę współrzędnych
            //Pobiera listę wszystkich rzeczy w skrypcie "mina"
            //SerializedObject serializedObjectGłówny = new SerializedObject(mina);
            //Pobiera wszystkie elementy przypisane do "listaWspórłrzędnych"
            SerializedProperty serializedlistaWspółrzędnych = serializedObjectGłówny.FindProperty("listaWspółrzędnych");
            //Aktualizuje wszelkie zmiany jakie wprowadzę w listaWspółrzędnych
            serializedObjectGłówny.Update();
            EditorGUILayout.PropertyField(serializedlistaWspółrzędnych, true);
            serializedObjectGłówny.ApplyModifiedProperties();
            #endregion
            //#region Tworzenie listy z prędkościami
            //SerializedProperty serializedPrędkosci = serializedObjectGłówny.FindProperty("prędkośćMiny");
            //serializedObjectGłówny.Update();
            //EditorGUILayout.PropertyField(serializedPrędkosci, true);
            //serializedObjectGłówny.ApplyModifiedProperties();
            //#endregion

            //Zapisuje zmiany w inspektorze
            if (GUI.changed) EditorUtility.SetDirty(mina);

        }
    }
}
