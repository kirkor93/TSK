using UnityEngine;

namespace Assets.Scripts
{
    public class Point3D : Point
    {
        public override void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public override void Draw(double concentration, double maxConcentration, double size)
        {
            transform.localScale *= (float)size;

            Material mat = GetComponent<Renderer>().material;
            PointColor.a = (float) (concentration/maxConcentration);
            mat.color = PointColor;
        }
    }
}
