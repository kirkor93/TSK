using System;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TerrainType { Water, Meadow, Farmland, Orchard, Forest, Village, City }
    public enum CityBuildingType { Small, Medium, High }
    public enum TimeOfYearType { Year, Winter, Summer }
    public enum AtmosphereConditionType { HighlyUnstable, Unstable, SlightlyUnstable, Indifferent, AlmostStable, Stable }

    public class GasSimulator : SimulatorBase
    {

        public override double Simulate(Vector3 position)
        {
            double m, z0, sigmaY, sigmaZ, a, A, b, B, result;

            m = AtmosphereConditionValue;
            z0 = TerrainRoughness;
            A = 0.08f*(6*Math.Pow(m, -0.3f) + 1.0f - Math.Log(EmissionHeight/z0));
            a = 0.367f*(2.5f - m);
            sigmaY = A*Math.Pow(position.x, a);

            B = 0.38f*Math.Pow(m, 1.3f)*(8.7 - Math.Log(EmissionHeight/z0));
            b = 1.55*Math.Exp(-2.35f*m);
            sigmaZ = B * Math.Pow(position.x, b);

            result = 1.0f/(sigmaY*sigmaZ);

            result *= Math.Exp(-(Math.Pow(position.z, 2)/(2*Math.Pow(sigmaZ, 2))));
            double mul = Math.Exp(-Math.Pow(position.y - EmissionHeight, 2)/(2.0f*Math.Pow(sigmaY, 2)));
            mul += Math.Exp(-Math.Pow(position.y + EmissionHeight, 2)/(2.0f*Math.Pow(sigmaY, 2)));
            result *= mul;

            result *= (1.0f/WindSpeed);
            result *= (EmissionIntensity / (2.0f * Mathf.PI));

            return result;
        }
    }
}
