using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MousePointConcentrationViewer : MonoBehaviour
    {
        public Camera SceneCamera;

        private int _mask;
        private RectTransform _rectTransform;
        private Text _text;

        protected void Awake()
        {
            _mask = LayerMask.GetMask("Point");
            _rectTransform = GetComponent<RectTransform>();
            _text = GetComponent<Text>();
        }

        protected void Update()
        {
            Ray ray = SceneCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue, _mask);
            Point p = null;
            foreach (RaycastHit hit in hits)
            {
                Point tmp = hit.transform.GetComponent<Point>();
                if (p == null
                    || tmp.Cp > p.Cp)
                {
                    p = tmp;
                }
            }

            _rectTransform.anchoredPosition = Input.mousePosition;
            if (p != null)
            {
                _text.text = string.Format("Pozycja: {0}\nCp: {1}", p.transform.position, GetCpString(p.Cp));
            }
            else
            {
                _text.text = string.Empty;
            }
        }

        public static string GetCpString(double baseCp)
        {
            int steps = 0;
            while (baseCp < 1.0f
                && steps < 12)
            {
                baseCp *= 10.0f;
                steps++;
            }

            string returnValue = "";

            if (steps < 2){}
            else if(steps < 5)
            {
                returnValue = "m";
                steps -= 3;
            }
            else if(steps < 8)
            {
                returnValue = "µ";
                steps -= 6;
            }
            else if(steps < 11)
            {
                returnValue = "n";
                steps -= 9;
            }
            else
            {
                returnValue = "p";
                steps -= 12;
            }
            returnValue += "g/m³";

            for (int i = 0; i < steps; i++)
            {
                baseCp /= 10.0f;
            }

            baseCp = Math.Round(baseCp, 2);
            returnValue = string.Concat(baseCp.ToString("G", CultureInfo.InvariantCulture), returnValue);
            return returnValue;
        }
    }
}
