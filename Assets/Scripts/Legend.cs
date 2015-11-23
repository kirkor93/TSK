using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Legend : MonoBehaviour
    {
        public Image CpBar;
        public Text CpMinText;
        public Text CpMaxText;
        public Text HeightText;

        public void Refresh(SimulationController simulationController)
        {
            CultureInfo info = CultureInfo.InvariantCulture;
            
            CpMinText.text = simulationController.CpMin.ToString("e2", info);
            CpMaxText.text = simulationController.CpMax.ToString("e2", info);
            ISimulationComponent tmp = (simulationController.SimulationComponent.GetComponent(typeof(ISimulationComponent)) as ISimulationComponent);
            CpBar.sprite = simulationController.Type == SimulationType.ThreeDimensional ? tmp.Bar3 : tmp.Bar2;
            HeightText.text = string.Format("Wysokość: {0}m ", simulationController.SectionHeight.ToString("G", info));
        }
    }
}
