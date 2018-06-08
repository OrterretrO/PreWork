using UnityEngine;


namespace Retro
{

    public interface FrameBuff
    {
        int width { get; }
        int height { get; }
    }


    public interface IRetroGraphic
    {
        FrameBuff CreateFrameBuff(int width, int height);

        FrameBuff CreateFrameBuff(string path);

        void SetPixel(FrameBuff buff, int x, int y, Color color);

        void DrawLine(FrameBuff buff, Vector2 start, Vector2 end, Color color);

        void DrawImage(FrameBuff buff, FrameBuff img, Rect srcRect, Rect destRect);

        void Clear(FrameBuff buff, Color color);

        void Swap();
    }

}