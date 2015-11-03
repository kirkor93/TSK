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

        public void Refresh(SimulationController simulationController)
        {
//            Texture2D tex = new Texture2D((int)CpBar.rectTransform.rect.width, (int)CpBar.rectTransform.rect.height, TextureFormat.ARGB32, false);
//            Color c = simulationController.GetComponentInChildren<Point>().PointColor;
//            for (int i = 0; i < tex.width; i++)
//            {
//                c.a = i/tex.width;
//
//                for (int j = 0; j < tex.height; j++)
//                {
//                    tex.SetPixel(i, j, c);
//                }
//            }
//            CpBar.sprite = Sprite.Create(tex, new Rect(Vector2.zero, Vector2.one), Vector2.zero);

            CultureInfo info = CultureInfo.InvariantCulture;
            
            CpMinText.text = simulationController.CpMin.ToString("F1", info);
            CpMaxText.text = simulationController.CpMax.ToString("F1", info);

        }
    }
}
