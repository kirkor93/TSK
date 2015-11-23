using System;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TerrainType { Water, Meadow, Farmland, Orchard, Forest, Village, City }
    public enum CityBuildingType { Small, Medium, High }
    public enum TimeOfYearType { Year, Winter, Summer }
    public enum AtmosphereConditionType { HighlyUnstable, Unstable, SlightlyUnstable, Indifferent, AlmostStable, Stable }

    public class GasSimulator : MonoBehaviour, ISimulationComponent
    {
        public TerrainType Terrain;
        public CityBuildingType CityBuilding;
        public TimeOfYearType TimeOfYear;
        [Range(1, int.MaxValue)]
        public int NumberOfCitizens = 10000;

        public AtmosphereConditionType AtmosphereCondition;
        [Range(1.0f, 11.0f)]
        public double WindSpeed = 1.0f;

        [Range(1.0f, 1000.0f)]
        public double EmissionHeight = 50.0f;
        [Range(1.0f, 1000.0f)]
        public double EmissionIntensity = 1.0f;

        public Color ComponentColor;

        public Color PointColor
        {
            get { return ComponentColor; }
        }

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

        public double TerrainRoughness
        {
            get
            {
                double result = 0.0f;
                switch (Terrain)
                {
                    case TerrainType.Water:
                        switch (TimeOfYear)
                        {
                            case TimeOfYearType.Year:
                                result = 0.00008f;
                                break;
                            case TimeOfYearType.Winter:
                                result = 0.00005f;
                                break;
                            case TimeOfYearType.Summer:
                                result = 0.0001f;
                                break;
                        }
                        break;
                    case TerrainType.Meadow:
                        switch (TimeOfYear)
                        {
                            case TimeOfYearType.Year:
                                result = 0.02f;
                                break;
                            case TimeOfYearType.Winter:
                                result = 0.001f;
                                break;
                            case TimeOfYearType.Summer:
                                result = 0.04f;
                                break;
                        }
                        break;
                    case TerrainType.Farmland:
                        switch (TimeOfYear)
                        {
                            case TimeOfYearType.Year:
                                result = 0.035f;
                                break;
                            case TimeOfYearType.Winter:
                                result = 0.001f;
                                break;
                            case TimeOfYearType.Summer:
                                result = 0.07f;
                                break;
                        }
                        break;
                    case TerrainType.Orchard:
                        result = 0.4f;
                        break;
                    case TerrainType.Forest:
                        result = 0.4f;
                        break;
                    case TerrainType.Village:
                        result = 0.5f;
                        break;
                    case TerrainType.City:
                        if (NumberOfCitizens < 10 * 1000)
                        {
                            result = 1.0f;
                        }
                        else if (NumberOfCitizens < 100 * 1000)
                        {
                            switch (CityBuilding)
                            {
                                case CityBuildingType.Small:
                                    result = 0.5f;
                                    break;
                                case CityBuildingType.Medium:
                                    result = 2.0f;
                                    break;
                            }
                        }
                        else if (NumberOfCitizens < 500 * 1000)
                        {
                            switch (CityBuilding)
                            {
                                case CityBuildingType.Small:
                                    result = 0.5f;
                                    break;
                                case CityBuildingType.Medium:
                                    result = 2.0f;
                                    break;
                                case CityBuildingType.High:
                                    result = 3.0f;
                                    break;
                            }
                        }
                        else
                        {
                            switch (CityBuilding)
                            {
                                case CityBuildingType.Small:
                                    result = 0.5f;
                                    break;
                                case CityBuildingType.Medium:
                                    result = 2.0f;
                                    break;
                                case CityBuildingType.High:
                                    result = 5.0f;
                                    break;
                            }
                        }
                        break;
                }
                return result;
            }
        }

        public double Simulate(Vector3 position)
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
