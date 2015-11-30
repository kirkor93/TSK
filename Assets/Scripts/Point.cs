using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Point : MonoBehaviour
    {
//        public Color PointColor = Color.red;

        private Sprite _sprite;

        protected const double MaxCpValue = 0.0001f;
        protected const double MinCpValue = 0.000000000001f;
        protected const double LogarithmBase = 10.0f;

        public double Cp { get; set; }

        public virtual void SetPosition(Vector3 position)
        {
            position.y = 0.0f;
            transform.position = position;
        }

        public virtual void Draw(double concentration, double size, Color pointColor)
        {
            if (_sprite == null)
            {
                CreateSprite(pointColor);
            }

            transform.localScale *= 100 * (float)size;

            Cp = concentration;

            SpriteRenderer ren = GetComponent<SpriteRenderer>();
            ren.sprite = _sprite;
            pointColor = Color.Lerp(Color.white, pointColor, ConvertCpToAlpha(concentration));
            ren.color = pointColor;
        }

        private void CreateSprite(Color pointColor)
        {
            Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            tex.SetPixel(0, 0, pointColor);
            _sprite = Sprite.Create(tex, new Rect(Vector2.zero, Vector2.one), Vector2.one * 0.5f);
        }

        protected static float ConvertCpToAlpha(double cp)
        {
            float newValue = Mathf.Clamp((float) cp, (float) MinCpValue, (float) MaxCpValue);

            return (float) ConvertRange(MinCpValue, MaxCpValue, 0.0f, 1.0f, newValue);
        }

        protected static double ConvertRange(double oldMin, double oldMax, double newMin, double newMax, double oldValue)
        {
//            Debug.Log((((oldValue - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin);
            return (((oldValue - oldMin)*(newMax - newMin))/(oldMax - oldMin)) + newMin;
        }
    }
}
