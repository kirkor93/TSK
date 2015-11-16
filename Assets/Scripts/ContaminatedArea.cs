using System;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TerrainType { Water, Meadow, Farmland, Orchard, Forest, Village, City }
    public enum CityBuildingType { Small, Medium, High }
    public enum TimeOfYearType { Year, Winter, Summer }

    public class ContaminatedArea : MonoBehaviour, ISimulationComponent
    {
        public TerrainType Terrain;
        public CityBuildingType CityBuilding;
        public TimeOfYearType TimeOfYear;
        [Range(1, int.MaxValue)]
        public int NumberOfCitizens = 10000;

        private Atmosphere _atmosphere;
        private Contamination _contamination;

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
            if (_atmosphere == null)
            {
                _atmosphere = FindObjectOfType<Atmosphere>();
            }
            if (_contamination == null)
            {
                _contamination = FindObjectOfType<Contamination>();
            }

            double m, z0, sigmaY, sigmaZ, a, A, b, B, result;

            m = _atmosphere.AtmosphereConditionValue;
            z0 = TerrainRoughness;
            A = 0.08f*(6*Math.Pow(m, -0.3f) + 1.0f - Math.Log(_contamination.EmissionHeight/z0));
            a = 0.367f*(2.5f - m);
            sigmaY = A*Math.Pow(position.x, a);

            B = 0.38f*Math.Pow(m, 1.3f)*(8.7 - Math.Log(_contamination.EmissionHeight/z0));
            b = 1.55*Math.Exp(-2.35f*m);
            sigmaZ = B * Math.Pow(position.x, b);

            result = 1.0f/(sigmaY*sigmaZ);

            result *= Math.Exp(-(Math.Pow(position.z, 2)/(2*Math.Pow(sigmaZ, 2))));
            double mul = Math.Exp(-Math.Pow(position.y - _contamination.EmissionHeight, 2)/(2.0f*Math.Pow(sigmaY, 2)));
            mul += Math.Exp(-Math.Pow(position.y + _contamination.EmissionHeight, 2)/(2.0f*Math.Pow(sigmaY, 2)));
            result *= mul;

            return result;
        }
    }
}
