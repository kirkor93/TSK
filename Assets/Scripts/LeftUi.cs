using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeftUi : MonoBehaviour
    {
        public SimulationController Simulator;

        public void SimulationTypeList(int value)
        {
            Simulator.Type = (SimulationType) value;
            Debug.Log((SimulationType)value);
        }

        public void RangeField(string text)
        {
            float value = 0;
            try
            {
                value = Convert.ToSingle(text);
            }
            catch (Exception)
            {
                Debug.LogError("Wrong number in field");
                return;
            }
            Vector3 min = Simulator.GridRangeMin;
            Vector3 max = Simulator.GridRangeMax;

            min.x = min.y = 0.0f;
            max.y = max.x = value;
            max.z = value/2.0f;
            min.z = -max.z;

            Simulator.GridRangeMax = max;
            Simulator.GridRangeMin = min;
        }

        public void DensityField(string text)
        {
            float value = 0;
            try
            {
                value = Convert.ToSingle(text);
            }
            catch (Exception)
            {
                Debug.LogError("Wrong number in field");
                return;
            }
            Simulator.GridDensity = value;
        }

        public void SectionDensityField(string text)
        {
            float value = 0;
            try
            {
                value = Convert.ToSingle(text);
            }
            catch (Exception)
            {
                Debug.LogError("Wrong number in field");
                return;
            }
            Simulator.SectionHeightStep = value;
        }
    }
}
