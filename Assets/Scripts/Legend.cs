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
        public Camera SceneCamera;
        public SimulationController Simulator;

        protected void Awake()
        {
            CultureInfo info = CultureInfo.InvariantCulture;
            CpMinText.text = (0.0f).ToString("G", info);
            CpMaxText.text = MousePointConcentrationViewer.GetCpString(Point.MaxCpValue * 10.0f);
        }

        protected void Update()
        {
            switch (Simulator.Type)
            {
                case SimulationType.TwoDimensional:
                    HeightText.text = string.Format("Symulowana wysokość: {0}", Simulator.SectionHeight);
                    break;
                case SimulationType.ThreeDimensional:
                    HeightText.text = string.Format("Pozycja kamery: {0}", SceneCamera.transform.position);
                    break;
            }
        }
    }
}
