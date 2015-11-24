using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SimulateButton : MonoBehaviour
    {
        public SimulationController Simulator;

        public void OnClick()
        {
            Text text = GetComponentInChildren<Text>();

            if (Simulator.IsSimulating)
            {
                Simulator.StopSimulation();
//                text.text = "Rozpocznij symulację";
            }
            else
            {
                Simulator.StartSimulation();
//                text.text = "Przerwij symulację";
            }
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
