using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


namespace Retro
{
    public class Image : IDisposable
    {
        private readonly int m_width;
        private readonly int m_height;
        public int Width => m_width;
        public int Height => m_height;

        internal RenderTexture InternalRtx;
        internal Texture2D InternalTex;


        private bool m_onDraw = false;
        private ImageDrawList m_drawList = new ImageDrawList();

        public Texture NativeTexture => InternalRtx == null ? InternalTex as Texture : InternalRtx;

        public Image(Texture2D texture)
        {
            m_width = texture.width;
            m_height = texture.height;

            InternalTex = texture;
        }

        public Image(int width, int height)
        {
            m_width = width;
            m_height = height;

            InternalTex = new Texture2D(width, height);
            InternalTex.filterMode = FilterMode.Point;
            InternalTex.Apply(false, false);
        }

        public void BeginDraw()
        {
            m_onDraw = true;
            m_drawList.Reset();
        }

        public void EndDraw()
        {
            m_drawList.Submit(this);
            m_onDraw = false;
        }

        public void SetPixel(int x, int y,Color color)
        {
            if (!m_onDraw) throw new Exception("setpixel must be called after BeginDraw()");
            m_drawList.SetPixel(x, y, color);
        }

        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {

        }

        public void DrawImage(Image img, Rectangle srcrect, Rectangle destrect)
        {
            GraphicContext.ImageDraw(this, img, srcrect, destrect);
        }

        public void Clear(Color color)
        {
            GraphicContext.ImageClear(this, color);
        }

        internal void InternalCheckTexture()
        {
            if (InternalRtx == null)
            {
                InternalRtx = RenderTexture.GetTemporary(Width, Height);
                InternalRtx.antiAliasing = 1;
                InternalRtx.filterMode = FilterMode.Point;
                Graphics.Blit(InternalTex, InternalRtx);
            }
        }


        public void Dispose()
        {
            GraphicContext.Release(this);
        }


    }

}

