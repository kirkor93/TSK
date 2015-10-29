using UnityEngine;

namespace Assets.Scripts
{
    public interface ISimulationComponent
    {
        double Simulate(Vector3 position);
    }
}
