using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Wyspa))]
public class WyspaInspetor : Editor
{
    public override void OnInspectorGUI()
    {
        Wyspa wyspa = (Wyspa) target;
        
        GUILayout.Label("Czy wyspa ma się poruszać: ");
        wyspa.CzyPoruszać = EditorGUILayout.Toggle("", wyspa.CzyPoruszać);
        if (wyspa.CzyPoruszać)
        {
            if (wyspa.ListaWspółrzędnych.Length == 0)
            {
                GUILayout.Label("Bierząca pozycja: 0");
            }
            else
            {
                GUILayout.Label("Bierząca pozycja: " + (wyspa.BierzącaPozycja + 1));
            }
            GUILayout.Label("Zapisanych pozycji: " + wyspa.ListaWspółrzędnych.Length);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Dodaj współrzędną"))
            {
                wyspa.DodajPozycjeMiny(wyspa.Prędkość);
            }
            if (GUILayout.Button("Edytuj Współrzędną"))
            {
                wyspa.EdytujPozycję();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Usuń ostatnią pozycję"))
            {
                wyspa.UsuńOstatniąPozycję();
            }
            if (GUILayout.Button("Usuń wszystkie współrzędne"))
            {
                wyspa.UsuńWszystkiePozycje();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Następna pozycja"))
            {
                wyspa.NastępnaPozycja();
            }
            if (GUILayout.Button("Poprzednia pozycja"))
            {
                wyspa.PoprzedniaPozycja();
            }
            GUILayout.EndHorizontal();
            wyspa.Prędkość = EditorGUILayout.FloatField("Prędkość przejazdu:", wyspa.Prędkość);
            GUILayout.Space(10);

        }
    }
}
