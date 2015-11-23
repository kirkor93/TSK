using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class DustSimulator : SimulatorBase
    {
        public float BetaAngle = 1.0f;
        public float BFactor = 0.5f;
        public float FallFactor = 1.0f;

        public override double Simulate(Vector3 position)
        {
            if (position.x < 100.0f)
            {
                position.x = 100.0f;
            }

            double m, z0, sigmaY, a, A, result;

            m = AtmosphereConditionValue;
            z0 = TerrainRoughness;
            A = 0.08f * (6 * Math.Pow(m, -0.3f) + 1.0f - Math.Log(EmissionHeight / z0));
            a = 0.367f * (2.5f - m);
            sigmaY = A * Math.Pow(position.x, a);

//            string message = "x: " + position.x;
            result = EmissionIntensity / (Math.Sqrt(2.0f * Math.PI) * BetaAngle);
//            message += "1: " + result;
            result *= ((1.0f - BFactor)*FallFactor*position.x + BFactor*WindSpeed*EmissionHeight);
//            message += " 2: " + result;
            result *= (1.0f/(WindSpeed*sigmaY*position.x*position.x));
//            message += " 3: " + result;
            result *= Math.Exp((-(FallFactor*position.x*Math.Sqrt(WindSpeed) - EmissionHeight))/(2.0f*sigmaY*sigmaY));
//            message += " 4: " + result;
//            Debug.Log(message);
            return result;
        }
    }
}
