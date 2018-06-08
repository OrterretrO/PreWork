using UnityEngine;


namespace Retro
{

    public class TextureGraph : IRetroGraph
    {
        /// <summary>
        /// 创建一个GraphicInterface对象
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        static public TextureGraph Create(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Point;

            GameObject cameraGo = new GameObject("GI");
            Camera camera = cameraGo.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = height / 2;
            camera.nearClipPlane = 0;

            SpriteRenderer spriteRender = cameraGo.AddComponent<SpriteRenderer>();
            spriteRender.flipY = true;
            spriteRender.sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(width, height)), new Vector2(0.5f, 0.5f), 1);

            return new TextureGraph(texture);
        }


        private Texture2D m_texture;


        private TextureGraph() { }

        private TextureGraph(Texture2D texture) { m_texture = texture; }

        /// <summary>
        /// 创建图形缓冲
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public FrameBuff CreateFrameBuff(int width, int height)
        {
            return new frameBuff(new Texture2D(width, height, TextureFormat.RGB24, false));
        }

        /// <summary>
        /// 读取已有图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FrameBuff CreateFrameBuff(string path)
        {
            return new frameBuff(Resources.Load<Texture2D>(path));
        }

        /// <summary>
        /// 画像素
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(FrameBuff buff, int x, int y, Color color)
        {
            if (buff == null)
                m_texture.SetPixel(x, y, color);
            else
                (buff as frameBuff).texture.SetPixel(x, y, color);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        public void DrawLine(FrameBuff buff, Vector2 start, Vector2 end, Color color)
        {
            Texture2D tex = buff == null ? m_texture : (buff as frameBuff).texture;

            if(start.x == end.x)
            {
                for (int i = (int)start.y; i != (int)end.y; i += System.Math.Sign(end.y - start.y))
                    tex.SetPixel((int)start.x, i, color);
            }
            else
            {
                for (int i = (int)start.x; i != (int)end.x; i += System.Math.Sign(end.x - start.x))
                    tex.SetPixel(i, (int)start.y, color);
            }
        }

        /// <summary>
        /// bitblt
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="img"></param>
        /// <param name="srcRect"></param>
        /// <param name="destRect"></param>
        public void DrawImage(FrameBuff buff, FrameBuff img, Rect srcRect, Rect destRect)
        {
            Texture2D tex = buff == null ? m_texture : (buff as frameBuff).texture;

            tex.SetPixels((int)destRect.x, (int)destRect.y, (int)destRect.width, (int)destRect.height,
                (img as frameBuff).texture.GetPixels((int)srcRect.x, (int)srcRect.y, (int)srcRect.width, (int)srcRect.height));
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="color"></param>
        public void Clear(FrameBuff buff, Color color)
        {
            Texture2D tex = buff == null ? m_texture : (buff as frameBuff).texture;

            Color[] fillColor = new Color[tex.height * tex.width];
            fillColor.Initialize();
            for (int i = 0; i < fillColor.Length; i++)
                fillColor[i] = color;

            tex.SetPixels(fillColor);
        }

        /// <summary>
        /// 绘制到屏幕
        /// </summary>
        public void Swap()
        {
            m_texture.Apply();
        }


        private class frameBuff : FrameBuff
        {
            public frameBuff(Texture2D tex)
            {
                texture = tex;
                width = texture.width;
                height = texture.height;
            }

            public Texture2D texture;

            public int width { get; private set; }
            public int height { get; private set; }
        }
    }
}
