using UnityEngine;

namespace Assets.Scripts
{
    public class LookingAtCamera : MonoBehaviour
    {
        public float BaseSpeed = 10.0f;

        private bool _rmbDown = false;
        private Vector3 _mousePosition = Vector3.zero;

        public float FasterModeMultiplier { get; set; }

        void Update ()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _rmbDown = true;
                _mousePosition = Input.mousePosition;
            }
            
            if(Input.GetMouseButtonUp(1))
            {
                _rmbDown = false;
            }


            if(_rmbDown)
            {
                Vector3 currentMousePosition = Input.mousePosition;
                float dx = currentMousePosition.x - _mousePosition.x;
                float dy = currentMousePosition.y - _mousePosition.y;
                _mousePosition = currentMousePosition;

                transform.Rotate(0.0f, dx, 0.0f, Space.World);
                transform.Rotate(-dy, 0.0f, 0.0f, Space.Self);
//                transform.Rotate(new Vector3(-dy, dx, 0.0f)/*, Space.World*/);

                Vector3 movement = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0.0f,
                    Input.GetAxis("Vertical")
                    );

                if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    movement *= FasterModeMultiplier;
                }

                movement = transform.rotation * movement;

                transform.position += movement * Time.deltaTime * 100.0f;
            }
        }
    }
}
