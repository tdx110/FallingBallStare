using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obiekty))]
public class ObiektyInspector : Editor
{

    public override void OnInspectorGUI()
    {
        Obiekty obiekty = (Obiekty) target;

        GUILayout.Label("Czy Miny maj¹ pozwolenie na poruszanie siê");
        obiekty.MoveMiny = EditorGUILayout.Toggle("Move Miny:", obiekty.MoveMiny);
        GUILayout.Label("Czy wyspy maj¹ pozwolenie na poruszanie siê");
        obiekty.MoveWyspy = EditorGUILayout.Toggle("Move Wyspy:", obiekty.MoveWyspy);
        GUILayout.Label("Kamera g³ówna do przeskalowania wysp");
        GUILayout.Label("¿eby pasowa³y do danego urz¹dzenia");
        obiekty.ResolutionCanvas = (RectTransform) EditorGUILayout.ObjectField(obiekty.ResolutionCanvas, typeof(RectTransform), true);


        if (GUI.changed)
        {
            EditorUtility.SetDirty(obiekty);
        }
    }
}
