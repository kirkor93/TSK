using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Transform BaseTransform;
        public Transform Transform3D;

        private Camera _camera;
        private SimulationType _currentSimulationType = SimulationType.TwoDimensional;

        protected void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void SetView(SimulationType type)
        {
            if (_currentSimulationType == type)
            {
                return;
            }

            _currentSimulationType = type;

            switch (type)
            {
                case SimulationType.TwoDimensional:
                    transform.position = BaseTransform.position;
                    transform.rotation = BaseTransform.rotation;
                    _camera.orthographic = true;
                    _camera.farClipPlane = 999999;
                    break;
                case SimulationType.ThreeDimensional:
                    transform.position = Transform3D.position;
                    transform.rotation = Transform3D.rotation;
                    _camera.orthographic = false;
                    _camera.farClipPlane = 60000;
                    break;
            }
        }
    }
}
