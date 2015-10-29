using UnityEngine;

namespace Assets.Scripts
{
    public class SimulationController : MonoBehaviour
    {
        public GameObject[] SimulationComponents;

        public void Simulate()
        {
            if (SimulationComponents == null)
            {
                Debug.LogError("There are no simulation components asigned! Fix it!");
                return;
            }

            ISimulationComponent[] simulationComponents = new ISimulationComponent[SimulationComponents.Length];

            for (int i = 0; i < SimulationComponents.Length; i++)
            {
                if (SimulationComponents[i] != null)
                {
                    simulationComponents[i] = (ISimulationComponent) SimulationComponents[i].GetComponent(typeof(ISimulationComponent));
                }
            }
        }
    }
}
