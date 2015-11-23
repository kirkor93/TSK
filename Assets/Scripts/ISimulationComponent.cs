using UnityEngine;

namespace Assets.Scripts
{
    public interface ISimulationComponent
    {
        Color PointColor { get; }
        Sprite Bar2 { get; }
        Sprite Bar3 { get; }

        double Simulate(Vector3 position);
    }
}
