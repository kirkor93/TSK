using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GasUi : MonoBehaviour
    {
        public SimulatorBase Simulator;
        public RectTransform CityBuildingListObject;
        public RectTransform CitizensNumberObject;
        public Text ValueText;

        public void TerrainTypeList(int value)
        {
            TerrainType newType = (TerrainType) value;
            Simulator.Terrain = newType;
            Debug.Log(newType);
            CityBuildingListObject.gameObject.SetActive(newType == TerrainType.City);
            CitizensNumberObject.gameObject.SetActive(newType == TerrainType.City);
        }

        public void CityBuildingList(int value)
        {
            Debug.Log((CityBuildingType)value);
            Simulator.CityBuilding = (CityBuildingType) value;
        }

        public void TimeOfYearList(int value)
        {
            Debug.Log((TimeOfYearType)value);
            Simulator.TimeOfYear = (TimeOfYearType) value;
        }

        public void NumberOfCitizensSlider(float value)
        {
            Slider slider = GetComponent<Slider>();
            float roundedValue = Mathf.Round(value);
            slider.value = roundedValue;

            ValueText.text = roundedValue.ToString("G", CultureInfo.InvariantCulture);

            Simulator.NumberOfCitizens = (int) roundedValue;
        }

        public void AtmosphereConditionList(int value)
        {
            Debug.Log((AtmosphereConditionType)value);
            Simulator.AtmosphereCondition = (AtmosphereConditionType) value;
        }

        public void WindSpeedSlider(float value)
        {
            Simulator.WindSpeed = value;
            ValueText.text = value.ToString("G", CultureInfo.InvariantCulture);
        }

        public void EmissionHeightSlider(float value)
        {
            Simulator.EmissionHeight = value;
            ValueText.text = value.ToString("G", CultureInfo.InvariantCulture);
        }

        public void EmissionIntensitySlider(float value)
        {
            Simulator.EmissionIntensity = value;
            ValueText.text = value.ToString("G", CultureInfo.InvariantCulture);
        }

        public void BetaAngleSlider(float value)
        {
            DustSimulator dust = Simulator as DustSimulator;
            if (dust != null)
            {
                dust.BetaAngle = value;
                ValueText.text = value.ToString("G", CultureInfo.InvariantCulture);
            }
        }

        public void FallFactorSlider(float value)
        {
            DustSimulator dust = Simulator as DustSimulator;
            if (dust != null)
            {
                dust.FallFactor = value;
                ValueText.text = value.ToString("G", CultureInfo.InvariantCulture);
            }
        }
    }
}
