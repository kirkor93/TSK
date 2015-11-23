using UnityEngine;

namespace Assets.Scripts
{
    public interface ISimulationComponent
    {
        Color PointColor { get; }

        double Simulate(Vector3 position);
    }
}
