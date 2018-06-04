using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Retro
{
    public sealed class FrameBuffer : IDisposable
    {

        private Image _backBuffer;
        private Image _frontBuffer;

        public Image Buffer => _backBuffer;

        public Texture FrameBuferTexture => _frontBuffer.NativeTexture;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            _backBuffer = new Image(width, height);
            _frontBuffer = new Image(width, height);
        }


        public void Swap()
        {
            var temp = _frontBuffer;
            _frontBuffer = _backBuffer;
            _backBuffer = _frontBuffer;
        }

        public void Dispose()
        {
            _backBuffer?.Dispose();
            _frontBuffer?.Dispose();
        }
    }

}
