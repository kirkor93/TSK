using UnityEngine;

namespace Assets.Scripts
{
    public class Point : MonoBehaviour
    {
//        public Color PointColor = Color.red;

        private Sprite _sprite;
        public double Cp { get; set; }

        public virtual void SetPosition(Vector3 position)
        {
            position.y = 0.0f;
            transform.position = position;
        }

        public virtual void Draw(double concentration, double maxConcentration, double size, Color pointColor)
        {
            if (_sprite == null)
            {
                CreateSprite(pointColor);
            }

            transform.localScale *= 100 * (float)size;

            Cp = concentration;

            SpriteRenderer ren = GetComponent<SpriteRenderer>();
            ren.sprite = _sprite;
            pointColor = Color.Lerp(Color.white, pointColor, (float) (concentration/maxConcentration));
//            PointColor.a = (float)(concentration/maxConcentration);
            ren.color = pointColor;
        }

        private void CreateSprite(Color pointColor)
        {
            Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            tex.SetPixel(0, 0, pointColor);
            _sprite = Sprite.Create(tex, new Rect(Vector2.zero, Vector2.one), Vector2.one * 0.5f);
        }
    }
}
