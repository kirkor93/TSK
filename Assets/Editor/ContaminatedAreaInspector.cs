using Assets.Scripts;
using UnityEditor;

[CustomEditor(typeof(ContaminatedArea))]
public class ContaminatedAreaInspector : Editor
{

    public override void OnInspectorGUI()
    {
        ContaminatedArea item = (ContaminatedArea) target;

        item.Terrain = (TerrainType) EditorGUILayout.EnumPopup("Terrain", item.Terrain);
        if (item.Terrain == TerrainType.City)
        {
            item.CityBuilding = (CityBuildingType) EditorGUILayout.EnumPopup("CityBuilding", item.CityBuilding);
            item.NumberOfCitizens = EditorGUILayout.IntField("NumberOfCitizens", item.NumberOfCitizens);
        }
    }
}
