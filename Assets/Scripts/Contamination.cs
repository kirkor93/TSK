﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Contamination : MonoBehaviour, ISimulationComponent
    {
        [Range(1.0f, 1000.0f)]
        public double EmissionHeight = 50.0f;
        [Range(1.0f, 1000.0f)]
        public double EmissionIntensity = 1.0f;

        public double Simulate(Vector3 position)
        {
            return 1.0f;
        }

        protected void OnValidate()
        {
            Vector3 pos = transform.position;
            pos.y = (float)EmissionHeight / 2.0f;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            scale.y = (float) EmissionHeight/2.0f;
            transform.localScale = scale;
        }
    }
}
