using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Gra))]
public class GraInspector : Editor
{

    public override void OnInspectorGUI()
    {
        Gra gra = (Gra)target;

        GUILayout.Label("Prosz� poda� obiekt do sterowania:");
        gra.ControllRigidbody2D = (Rigidbody2D) EditorGUILayout.ObjectField(gra.ControllRigidbody2D, typeof(Rigidbody2D),true);
        GUILayout.Label("Prosz� poda� si�� dzia�aj�c� na obiekt, podczas sterowania:");
        gra.PowerForce = EditorGUILayout.FloatField("Power force:", gra.PowerForce);
        GUILayout.Label("Nazwa bierz�cego poziomu:");
        gra.ThisLevelName = EditorGUILayout.TextField("This level name:", gra.ThisLevelName);
        GUILayout.Label("Czy plansza ma przej�cie na nast�pny poziom?:");
        gra.ShowNextLevel = EditorGUILayout.Toggle("Next level", gra.ShowNextLevel);
        if(gra.ShowNextLevel)
        {
            GUILayout.Label("Nazwa sceny nast�pnego poziomu");
            gra.NextLevelSceneName = EditorGUILayout.TextField("Next scene name:", gra.NextLevelSceneName);
        }


        if (GUI.changed) EditorUtility.SetDirty(gra);
    }

}
