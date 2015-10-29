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
    }
}
