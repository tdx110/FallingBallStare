using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Kamera))]
public class KameraInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Kamera kamera = (Kamera)target;
        //Pobiera list� wszystkich rzeczy w skrypcie "mina"
        SerializedObject serializedObjectKamera = new SerializedObject(kamera);
        kamera.Ball = (GameObject) EditorGUILayout.ObjectField(kamera.Ball, typeof(GameObject), true);
        if (kamera.imageBackground == null) kamera.imageBackground = GameObject.Find("T�o").GetComponent<Image>();

        #region Informacje o kamerze
        GUILayout.Label("Rodzaje poruszania si� kamery.");
        kamera.KameraMoreInfo = EditorGUILayout.Toggle("Wi�cej Info:", kamera.KameraMoreInfo);
        if (kamera.KameraMoreInfo)
        {
            GUILayout.Label("Nothing - Kamera nic nie robi. Dobre do pisania w�asnego skryptu");
            GUILayout.Label("PunktToPunkt - Kamera pod��a wed�ug listy punkt�w");
            GUILayout.Label("FollowPlayer - Kamera pod��a za graczem");
        }
        #endregion
        #region Zachowanie Kamery i Przyciski
        kamera.StartWork = EditorGUILayout.Toggle("Czy Uruchomiona/Pauza", kamera.StartWork);
        kamera.TypeKamera = (Kamera.CoRobi�Kamera)EditorGUILayout.EnumPopup("Zachowanie Kamery:", kamera.TypeKamera);
        switch (kamera.TypeKamera)
        {
            case Kamera.CoRobi�Kamera.Nothing:
                break;
            case Kamera.CoRobi�Kamera.PunktToPunkt:
                #region Punkt To Punkt
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("How many steps:" + kamera.PointList.Length);
                GUILayout.Label("Actual point:" + kamera.StepsPointList);
                EditorGUILayout.EndHorizontal();
                kamera.ActualSpeed = EditorGUILayout.FloatField("Actual speed:", kamera.ActualSpeed);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add point")) kamera.AddPoint();
                if (GUILayout.Button("Remove last point")) kamera.RemoveLastPoint();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Correct point pos.")) kamera.CorrectPoint();
                if (GUILayout.Button("Erase all point")) kamera.ErasePointList();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Next point")) kamera.ChangeStep(false);
                if (GUILayout.Button("Previous point")) kamera.ChangeStep(true);
                EditorGUILayout.EndHorizontal();
                kamera.DeltaDistance = (float)(Math.Round(kamera.DeltaDistance * 20) * 0.05); // 20*0,05 = 1!
                kamera.DeltaDistance = EditorGUILayout.Slider("Pos ball Y to Start:", kamera.DeltaDistance, -2, 2);
                #endregion
                break;
            case Kamera.CoRobi�Kamera.FollowPlayer:
                #region Follow Player
                kamera.ActualSpeed = EditorGUILayout.FloatField("Follow speed:", kamera.ActualSpeed);
                kamera.DeltaDistance = (float)(Math.Round(kamera.DeltaDistance * 20) * 0.05); // 20*0,05 = 1!
                kamera.DeltaDistance = EditorGUILayout.Slider("Start delta distance:", kamera.DeltaDistance, 0, 1);
                kamera.StartWorkPosition = (float)(Math.Round(kamera.StartWorkPosition * 20) * 0.05); // 20*0,05 = 1!
                kamera.StartWorkPosition = EditorGUILayout.Slider("Start delta distance:", kamera.StartWorkPosition, -2, 0);
                #endregion
                break;
            default:
                break;
        }
        #endregion
        #region Zachowanie t�a
        GUILayout.Space(20);
        GUILayout.Label("Kolor t�a");
        kamera.imageBackground.color = EditorGUILayout.ColorField("Background Color:", kamera.imageBackground.color);
        #endregion


        if (GUI.changed)
        {
            EditorUtility.SetDirty(kamera);
        }
    }
}
