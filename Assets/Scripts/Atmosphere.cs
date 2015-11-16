using UnityEngine;

namespace Assets.Scripts
{
    public enum AtmosphereConditionType { HighlyUnstable, Unstable, SlightlyUnstable, Indifferent, AlmostStable, Stable }

    public class Atmosphere : MonoBehaviour, ISimulationComponent
    {
        public AtmosphereConditionType AtmosphereCondition;
        [Range(1.0f, 11.0f)]
        public double WindSpeed = 1.0f;

        public double AtmosphereConditionValue
        {
            get
            {
                double result = 0.0f;
                switch (AtmosphereCondition)
                {
                    case AtmosphereConditionType.AlmostStable:
                        result = 0.363f;
                        break;
                    case AtmosphereConditionType.HighlyUnstable:
                        result = 0.080f;
                        break;
                    case AtmosphereConditionType.Indifferent:
                        result = 0.270f;
                        break;
                    case AtmosphereConditionType.SlightlyUnstable:
                        result = 0.196f;
                        break;
                    case AtmosphereConditionType.Stable:
                        result = 0.440f;
                        break;
                    case AtmosphereConditionType.Unstable:
                        result = 0.143f;
                        break;
                }
                return result;
            }
        }

        public double Simulate(Vector3 position)
        {
//            Debug.Log(1.0f / WindSpeed);
            return 1.0f / WindSpeed;
        }
    }
}
