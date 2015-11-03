using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public enum SimulationType { TwoDimensional, ThreeDimensional }

    [Serializable]
    public class PointTypePair
    {
        public SimulationType Type;
        public Point Point;
    }

    public class SimulationController : MonoBehaviour
    {
        public Vector3 GridRangeMin = Vector3.zero;
        public Vector3 GridRangeMax = Vector3.zero;
        public float GridDensity = 1.0f;
        public SimulationType Type = SimulationType.TwoDimensional;
        public float SectionHeight = 50.0f;

        public GameObject[] SimulationComponents;
        public PointTypePair[] PointPrefabs;

        public Legend Gui;

        public double CpMax { get; private set; }
        public double CpMin { get; private set; }
        
        public void Simulate()
        {
            CpMax = 0.0f;
            CpMin = double.MaxValue;

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

            if (Type == SimulationType.TwoDimensional)
            {
                GridRangeMin.y = GridRangeMax.y = SectionHeight;
            }

            Point pointPrefab = PointPrefabs.First(p => p.Type == Type).Point;

            Point[] oldPoints = GetComponentsInChildren<Point>(true);
            foreach (Point oldPoint in oldPoints)
            {
                DestroyImmediate(oldPoint.gameObject);
            }

            List<KeyValuePair<Point, double>> instantiatedPoints = new List<KeyValuePair<Point, double>>();

            for (float x = GridRangeMin.x; x <= GridRangeMax.x; x += GridDensity)
            {
                for (float y = GridRangeMin.y; y <= GridRangeMax.y; y += GridDensity)
                {
                    for (float z = GridRangeMin.z; z <= GridRangeMax.z; z += GridDensity)
                    {
                        Vector3 pos = new Vector3(x, y, z);

                        double result = 1.0f;
                        foreach (ISimulationComponent component in simulationComponents)
                        {
                            result *= component.Simulate(pos);
                        }
                        result = Random.Range(0.0f, 100.0f);
                        if (result > CpMax)
                        {
                            CpMax = result;
                        }
                        else if (result < CpMin)
                        {
                            CpMin = result;
                        }


                        GameObject point = Instantiate(pointPrefab.gameObject);
                        Point pointComponent = point.GetComponent<Point>();
                        pointComponent.SetPosition(pos);
                        point.transform.parent = transform;
                        instantiatedPoints.Add(new KeyValuePair<Point, double>(pointComponent, result));
                    }
                }
            }

            foreach (KeyValuePair<Point, double> point in instantiatedPoints)
            {
                point.Key.Draw(point.Value, CpMax, GridDensity);
            }

            Gui.Refresh(this);
        }
    }
}
