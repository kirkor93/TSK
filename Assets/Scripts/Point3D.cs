using UnityEngine;

namespace Assets.Scripts
{
    public class Point3D : Point
    {
        public override void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public override void Draw(double concentration, double size, Color pointColor)
        {
            transform.localScale *= (float)size;

            Material mat = GetComponent<Renderer>().material;
            pointColor.a = ConvertCpToAlpha(concentration);
            Cp = concentration;
            if (pointColor.a > 0)
            {
                mat.color = pointColor;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}
