

public class FrameBuff
{
    public int width;
    public int height;
}

public class Image
{
    public int width;
    public int height;
}


public interface GraphicInterface
{
    void SetPixel(FrameBuff buff, int x, int y, int color);

    void DrawLine(FrameBuff buff, object start, object end, int color);

    void DrawImage(FrameBuff buff, Image img, object srcRect, object destRect);

    void Clear(FrameBuff buff, int color);

    void Swap();
}

