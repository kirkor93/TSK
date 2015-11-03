using System.Globalization;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimulationController))]
public class SimulationControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Separator();

        SimulationController item = (SimulationController) target;

        if (GUILayout.Button("Start simulation"))
        {
            item.Simulate();
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Results");
        EditorGUILayout.Separator();
        CultureInfo info = CultureInfo.InvariantCulture;
        EditorGUILayout.LabelField("Max Cp:", item.CpMax.ToString("G", info));
        EditorGUILayout.LabelField("Min Cp:", item.CpMin.ToString("G", info));
    }
}
