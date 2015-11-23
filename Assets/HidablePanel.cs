﻿using UnityEngine;

namespace Assets
{
    public class HidablePanel : MonoBehaviour
    {
        public RectTransform Panel;

        protected void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Panel.gameObject.SetActive(!Panel.gameObject.activeInHierarchy);
            }
        }
    }
}
