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

        GUILayout.Label("Proszê podaæ obiekt do sterowania:");
        gra.ControllRigidbody2D = (Rigidbody2D) EditorGUILayout.ObjectField(gra.ControllRigidbody2D, typeof(Rigidbody2D),true);
        GUILayout.Label("Proszê podaæ si³ê dzia³aj¹c¹ na obiekt, podczas sterowania:");
        gra.PowerForce = EditorGUILayout.FloatField("Power force:", gra.PowerForce);
        GUILayout.Label("Nazwa bierz¹cego poziomu:");
        gra.ThisLevelName = EditorGUILayout.TextField("This level name:", gra.ThisLevelName);
        GUILayout.Label("Czy plansza ma przejœcie na nastêpny poziom?:");
        gra.ShowNextLevel = EditorGUILayout.Toggle("Next level", gra.ShowNextLevel);
        if(gra.ShowNextLevel)
        {
            GUILayout.Label("Nazwa sceny nastêpnego poziomu");
            gra.NextLevelSceneName = EditorGUILayout.TextField("Next scene name:", gra.NextLevelSceneName);
        }


        if (GUI.changed) EditorUtility.SetDirty(gra);
    }

}
