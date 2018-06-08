using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Retro
{

    public class ImageDrawList
    {

        private List<Vector2> m_vert = new List<Vector2>();
        private List<Color> m_col = new List<Color>();

        public void Reset()
        {
            m_vert.Clear();
            m_col.Clear();
        }

        public void SetPixel(int x, int y, Color color)
        {
            m_vert.Add(new Vector2(x, y));
            m_col.Add(color);
        }


        public void Submit(Image img)
        {
            if (m_vert.Count == 0 || m_col.Count == 0) return;
            GraphicContext.DrawPixels(img, m_vert, m_col);

            m_vert.Clear();
            m_col.Clear();

        }
    }


}
