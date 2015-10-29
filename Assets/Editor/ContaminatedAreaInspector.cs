using Assets.Scripts;
using UnityEditor;

[CustomEditor(typeof(ContaminatedArea))]
public class ContaminatedAreaInspector : Editor
{

    public override void OnInspectorGUI()
    {
        ContaminatedArea item = (ContaminatedArea) target;

        item.Terrain = (TerrainType) EditorGUILayout.EnumPopup(item.Terrain);
        if (item.Terrain == TerrainType.City)
        {
            item.CityBuilding = (CityBuildingType) EditorGUILayout.EnumPopup(item.CityBuilding);
            item.NumberOfCitizens = EditorGUILayout.IntField(item.NumberOfCitizens);
        }
    }
}
