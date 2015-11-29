using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MousePointConcentrationViewer : MonoBehaviour
    {
        private int _mask;
        private RaycastHit _hit;
        private double _cp;
        private RectTransform _rectTransform;

        protected void Awake()
        {
            _mask = LayerMask.GetMask("Point");
            _rectTransform = GetComponent<RectTransform>();
        }

        protected void Update()
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = Camera.main.transform.forward;
            if(Physics.Raycast(new Ray(origin, direction), out _hit, float.MaxValue, _mask))
            {
                Point p = _hit.transform.GetComponent<Point>();
                _cp = p.Cp;
            }
            else
            {
                _cp = 0.0f;
            }

            _rectTransform.anchoredPosition = Input.mousePosition;
        }
    }
}
