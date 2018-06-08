using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro
{

    public static class GraphicContext
    {
        private static Material s_matColor;
        private static int s_shaderIdColor;

        private static Material s_matPixel;
        private static ComputeBuffer s_bufferPoints;
        private static ComputeBuffer s_bufferColor;
        private static int s_shaderIdBufferPointer;
        private static int s_shaderIdBufferColors;

        private static int s_shaderIdImageSize;

        public static void Init()
        {
            var shader = Shader.Find("Retro/Color");
            s_matColor = new Material(shader);
            s_shaderIdColor = Shader.PropertyToID("_Color");


            var pixelshader = Shader.Find("Retro/Pixel");
            s_matPixel = new Material(pixelshader);
            s_shaderIdBufferPointer = Shader.PropertyToID("points");
            s_shaderIdBufferColors = Shader.PropertyToID("colors");
            s_bufferPoints = new ComputeBuffer(512,sizeof(float) *2, ComputeBufferType.Default);
            s_bufferColor = new ComputeBuffer(512, sizeof(float) * 4, ComputeBufferType.Default);
            s_matPixel.SetBuffer(s_shaderIdBufferPointer, s_bufferPoints);
            s_matPixel.SetBuffer(s_shaderIdBufferColors, s_bufferColor);

            s_shaderIdImageSize = Shader.PropertyToID("imgsize");

        }

        public static void Release()
        {
            s_bufferPoints?.Release();
            s_bufferColor?.Release();
        }

        public static void Release(Image img)
        {
            if (img.InternalRtx != null)
            {
                RenderTexture.ReleaseTemporary(img.InternalRtx);
                img.InternalRtx = null;
            }
        }

        public static void ImageClear(Image img,Color color)
        {
            img.InternalCheckTexture();

            s_matColor.SetColor(s_shaderIdColor, color);
            Graphics.Blit(null,img.InternalRtx, s_matColor);
        }

        public static void DrawPixels(Image img, List<Vector2> vert, List<Color> col)
        {
            if (vert.Count > 512) throw new System.Exception("SetPixel batch size exceeded!");

            s_bufferPoints.SetData(vert, 0, 0, vert.Count);
            s_bufferColor.SetData(col, 0, 0, col.Count);

            img.InternalCheckTexture();

            Graphics.SetRenderTarget(img.InternalRtx);
            s_matPixel.SetVector(s_shaderIdImageSize, new Vector4(img.Width, img.Height, 0, 0));
            s_matPixel.SetPass(0);
            Graphics.DrawProcedural(MeshTopology.Points, vert.Count);

        }

        public static void ImageDraw(Image dstimg, Image srcimg, Rectangle src, Rectangle dst)
        {
            dstimg.InternalCheckTexture();
            srcimg.InternalCheckTexture();

            int maxdstx = dstimg.Width - src.Width;
            int maxdsty = dstimg.Height - src.Height;
            if (dst.X > maxdstx)
            {
                dst.X = maxdstx;
            }
            if (dst.Y > maxdsty)
            {
                dst.Y = maxdsty;
            }


            Graphics.CopyTexture(
                srcimg.InternalRtx, 
                0, 0, 
                src.X,
                src.Y,
                src.Width,
                src.Height, 
                dstimg.InternalRtx, 
                0, 0,
                dst.X,
                dst.Y
                );
        }
    }

}

