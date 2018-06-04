using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro
{

    public static class GraphicContext
    {
        private static Material s_matColor;
        private static int s_shaderIdColor;

        public static void Init()
        {
            var shader = Shader.Find("Retro/Color");
            s_matColor = new Material(shader);
            s_shaderIdColor = Shader.PropertyToID("_Color");
        }

        public static void Release()
        {

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

