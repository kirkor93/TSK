using UnityEngine;

namespace Assets.Scripts
{
    public enum TerrainType { Water, Meadow, Farmland, Orchard, Forest, Village, City }
    public enum CityBuildingType { Small, Medium, High }

    public class ContaminatedArea : MonoBehaviour, ISimulationComponent
    {
        public TerrainType Terrain;
        public CityBuildingType CityBuilding;
        public int NumberOfCitizens = 10000;


        public double Simulate(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
