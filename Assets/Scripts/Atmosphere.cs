using UnityEngine;

namespace Assets.Scripts
{
    public enum AtmosphereConditionType { HighlyUnstable, Unstable, SlightlyUnstable, Indifferent, AlmostStable, Stable }

    public class Atmosphere : MonoBehaviour, ISimulationComponent
    {
        public AtmosphereConditionType AtmosphereCondition;
        [Range(1.0f, 11.0f)]
        public double WindSpeed = 1.0f;

        public double Simulate(Vector3 position)
        {
            return 1.0f;
        }
    }
}
