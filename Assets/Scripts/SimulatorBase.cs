using UnityEngine;

namespace Assets.Scripts
{
    public enum TerrainType { Water, Meadow, Farmland, Orchard, Forest, Village, City }
    public enum CityBuildingType { Small, Medium, High }
    public enum TimeOfYearType { Year, Winter, Summer }
    public enum AtmosphereConditionType { HighlyUnstable, Unstable, SlightlyUnstable, Indifferent, AlmostStable, Stable }

    public abstract class SimulatorBase : MonoBehaviour, ISimulationComponent
    {
        public Sprite Bar2D;
        public Sprite Bar3D;
        public Color ComponentColor;
        public GameObject[] NormalTerrainVisuals;
        public GameObject[] CityTerrainVisuals;

        private TerrainType _terrain;
        private CityBuildingType _cityBuilding;
        private TimeOfYearType _timeOfYear;
        private int _numberOfCitizens = 10000;
        private AtmosphereConditionType _atmosphereCondition;
        private double _windSpeed = 1.0f;
        private double _emissionHeight = 50.0f;
        private double _emissionIntensity = 1.0f;

        public TerrainType Terrain
        {
            get { return _terrain; }
            set
            {
                _terrain = value;
                ChangeTerrainVisual();
            }
        }

        public CityBuildingType CityBuilding
        {
            get { return _cityBuilding; }
            set
            {
                _cityBuilding = value;
                ChangeTerrainVisual();
            }
        }

        public TimeOfYearType TimeOfYear
        {
            get { return _timeOfYear; }
            set { _timeOfYear = value; }
        }

        public int NumberOfCitizens
        {
            get { return _numberOfCitizens; }
            set { _numberOfCitizens = value; }
        }

        public AtmosphereConditionType AtmosphereCondition
        {
            get { return _atmosphereCondition; }
            set { _atmosphereCondition = value; }
        }

        public double WindSpeed
        {
            get { return _windSpeed; }
            set { _windSpeed = value; }
        }

        public double EmissionHeight
        {
            get { return _emissionHeight; }
            set { _emissionHeight = value; }
        }

        public double EmissionIntensity
        {
            get { return _emissionIntensity; }
            set { _emissionIntensity = value; }
        }

        public Color PointColor
        {
            get { return ComponentColor; }
        }

        public Sprite Bar3
        {
            get { return Bar3D; }
        }

        public Sprite Bar2
        {
            get { return Bar2D; }
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

        private void ChangeTerrainVisual()
        {
            Debug.Log("Changing visuals");
            foreach (GameObject o in CityTerrainVisuals)
            {
                o.SetActive(false);
            }
            foreach (GameObject o in NormalTerrainVisuals)
            {
                o.SetActive(false);
            }

            
            if (Terrain != TerrainType.City)
            {
                NormalTerrainVisuals[(int)Terrain].SetActive(true);
            }
            else
            {
                CityTerrainVisuals[(int)CityBuilding].SetActive(true);
            }
        }

        public abstract double Simulate(Vector3 position);
    }
}
