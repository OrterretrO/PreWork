using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


namespace Retro
{
    public class Image : IDisposable
    {
        private readonly int _width;
        private readonly int _height;
        public int Width => _width;
        public int Height => _height;

        internal RenderTexture InternalRtx;
        internal Texture2D InternalTex;

        public Texture NativeTexture => InternalRtx == null ? InternalTex as Texture : InternalRtx;

        public Image(Texture2D texture)
        {
            _width = texture.width;
            _height = texture.height;

            InternalTex = texture;
        }

        public Image(int width, int height)
        {
            _width = width;
            _height = height;

            InternalTex = new Texture2D(width, height);
            InternalTex.Apply(false, false);
        }


        public void BeginDraw()
        {

        }

        public void EndDraw()
        {

        }

        public void SetPixel(int x, int y, Color32 color)
        {

        }

        public void DrawLine(int x1, int y1, int x2, int y2, Color32 color)
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
                Graphics.Blit(InternalTex, InternalRtx);
            }
        }


        public void Dispose()
        {
            GraphicContext.Release(this);
        }


    }

}

