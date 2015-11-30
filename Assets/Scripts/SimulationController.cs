using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

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
        public float SectionHeightMax = 1000.0f;
        public float SectionHeightStep = 50.0f;
        public float SimulationTimeInterval = 5.0f;

        public GameObject SimulationComponent;
        public PointTypePair[] PointPrefabs;

        public Legend Gui;
        public Camera SceneCamera;
        public SpriteRenderer ScaleBar;
        public TextMesh ScaleValue;
        public Transform Source;

        public double CpMax { get; private set; }
        public double CpMin { get; private set; }

        public bool IsSimulating { get; private set; }

        public void Simulate()
        {
//            CpMax = 0.01f;
//            CpMin = 0.0f;

            CpMin = double.MaxValue;
            CpMax = 0.0f;

            if (SimulationComponent == null)
            {
                Debug.LogError("There are no simulation components asigned! Fix it!");
                return;
            }

            ISimulationComponent simulationComponent = SimulationComponent.GetComponent(typeof(ISimulationComponent)) as ISimulationComponent;

            if (simulationComponent == null)
            {
                Debug.LogError("No simulation component assigned. Fix it");
            }

            Vector3 tmp = Vector3.zero, tmp2 = Vector3.zero;

            if (Type == SimulationType.TwoDimensional)
            {
                tmp = GridRangeMax;
                tmp2 = GridRangeMin;
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

                        double result = simulationComponent.Simulate(pos);
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

//            double scaledCpMax = 0.00000001f;
//            while (scaledCpMax < CpMax)
//            {
//                scaledCpMax *= 10.0f;
//            }
//            scaledCpMax /= 10.0f;
//            CpMax = scaledCpMax;


            foreach (KeyValuePair<Point, double> point in instantiatedPoints)
            {
                point.Key.Draw(point.Value, GridDensity, simulationComponent.PointColor);
            }

            float targetScale = Math.Min(GridRangeMax.x, GridRangeMax.z);
            float barScale = 1.0f;
            while (barScale < targetScale)
            {
                barScale *= 10.0f;
            }
            barScale /= 10.0f;
            ScaleBar.transform.localScale = Vector3.one*barScale;
            ScaleValue.text = barScale.ToString("G", CultureInfo.InvariantCulture) + " m";
            Source.localScale = Vector3.one*barScale * 0.4f;

            if (Type == SimulationType.TwoDimensional)
            {
                SceneCamera.orthographicSize = Math.Max(GridRangeMax.x, GridRangeMax.z);
            }
            CameraController controller = SceneCamera.GetComponent<CameraController>();
            controller.SetView(Type);
            SceneCamera.GetComponent<LookingAtCamera>().FasterModeMultiplier = barScale / 10.0f;

            if (Type == SimulationType.TwoDimensional)
            {
                GridRangeMax = tmp;
                GridRangeMin = tmp2;
            }

            Gui.Refresh(this);
        }

        public void StartSimulation()
        {
            if (Type == SimulationType.TwoDimensional
                && (SimulationComponent.GetComponent<GasSimulator>() != null))
            {
                StartCoroutine(SimulateLoop());
                IsSimulating = true;
            }
            else
            {
                SectionHeight = 0.0f;
                Simulate();
            }
        }

        public void StopSimulation()
        {
            StopAllCoroutines();
            IsSimulating = false;
        }

        private IEnumerator SimulateLoop()
        {
            SectionHeight = 0.0f;
            while (true)
            {
                Simulate();

                SectionHeight += SectionHeightStep;
                if (SectionHeight > SectionHeightMax)
                {
                    SectionHeight = 0.0f;
                }
                yield return new WaitForSeconds(SimulationTimeInterval);
            }
        }
    }
}
