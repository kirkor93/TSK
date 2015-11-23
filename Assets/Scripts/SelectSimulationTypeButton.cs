using UnityEngine;

namespace Assets.Scripts
{
    public class SelectSimulationTypeButton : MonoBehaviour
    {
        public RectTransform GasPanel;
        public RectTransform DustPanel;
        public SimulationController Simulation;
        public GameObject GasSimulator;
        public GameObject DustSimulator;

        public void OnValueChange(int newValue)
        {
            switch (newValue)
            {
                case 0:
                    Simulation.SimulationComponent = GasSimulator;
                    GasPanel.gameObject.SetActive(true);
                    DustPanel.gameObject.SetActive(false);
                    break;
                case 1:
                    Simulation.SimulationComponent = DustSimulator;
                    GasPanel.gameObject.SetActive(false);
                    DustPanel.gameObject.SetActive(true);
                    break;
                default:
                    GasPanel.gameObject.SetActive(false);
                    DustPanel.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
