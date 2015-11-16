using UnityEngine;

namespace Assets.Scripts
{
    public class Point : MonoBehaviour
    {
        public Color PointColor = Color.red;

        private Sprite _sprite;

        public virtual void SetPosition(Vector3 position)
        {
            position.y = 0.0f;
            transform.position = position;
        }

        public virtual void Draw(double concentration, double maxConcentration, double size)
        {
            if (_sprite == null)
            {
                CreateSprite();
            }

            transform.localScale *= 100 * (float)size;

            SpriteRenderer ren = GetComponent<SpriteRenderer>();
            ren.sprite = _sprite;
            PointColor = Color.Lerp(Color.white, PointColor, (float) (concentration/maxConcentration));
//            PointColor.a = (float)(concentration/maxConcentration);
            ren.color = PointColor;
        }

        private void CreateSprite()
        {
            Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            tex.SetPixel(0, 0, PointColor);
            _sprite = Sprite.Create(tex, new Rect(Vector2.zero, Vector2.one), Vector2.one * 0.5f);
        }
    }
}
