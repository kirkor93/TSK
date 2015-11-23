using UnityEngine;

namespace Assets.Scripts
{
    public class DustSimulator : MonoBehaviour, ISimulationComponent
    {
        public Color SimulationColor = Color.blue;

        public Color PointColor
        {
            get { return SimulationColor; }
        }

        public double Simulate(Vector3 position)
        {
            return 0.0f;
        }
    }
}
