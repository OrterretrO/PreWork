using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Retro;

public class RetroSample : MonoBehaviour
{


    private FrameBuffer m_framebuffer;

    public MeshRenderer m_meshRender;

    private Image m_textImg;

    private void Awake()
    {
        GraphicContext.Init();
    }

    private void OnDestroy()
    {
        m_framebuffer.Dispose();
        GraphicContext.Release();
    }

    void Start()
    {
        m_framebuffer = new FrameBuffer(120, 90);
        var color = new Color(Random.value, Random.value, Random.value);
        m_framebuffer.Buffer.Clear(color);

        m_meshRender.material.mainTexture = m_framebuffer.Buffer.NativeTexture;

        m_textImg = new Image(30, 10);

    }

    void Update()
    {

        //Sample1();
        Sample2();

        m_framebuffer.Swap();
        m_meshRender.material.mainTexture = m_framebuffer.Buffer.NativeTexture;
    }

    private void Sample1()
    {
        var posx = Mathf.FloorToInt(Random.value * 400);
        var posy = Mathf.FloorToInt(Random.value * 300);

        m_framebuffer.Buffer.DrawImage(
            m_textImg,
            new Rectangle(0, 0, 10, 10),
            new Rectangle(posx, posy, 10, 10)
        );
    }

    private void Sample2()
    {
        var buffer = m_framebuffer.Buffer;
        buffer.BeginDraw();

        for (var i = 0; i < 100; i++)
        {
            buffer.SetPixel(Mathf.FloorToInt(Random.value * 120), Mathf.FloorToInt(Random.value * 90), Color.blue);
        }
        buffer.EndDraw();
    }
}
