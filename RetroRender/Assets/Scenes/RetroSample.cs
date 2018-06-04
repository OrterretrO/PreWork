using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Retro;

public class RetroSample : MonoBehaviour
{


    private FrameBuffer _framebuffer;

    public MeshRenderer _meshRender;

    private Image _textImg;

    private void Awake()
    {
        GraphicContext.Init();
    }

    private void OnDestroy()
    {
        _framebuffer.Dispose();
        GraphicContext.Release();
    }

    void Start()
    {
        _framebuffer = new FrameBuffer(400, 300);
        var color = new Color(Random.value, Random.value, Random.value);
        _framebuffer.Buffer.Clear(color);

        _meshRender.material.mainTexture = _framebuffer.Buffer.NativeTexture;

        _textImg = new Image(30, 10);

    }

    void Update()
    {

        var posx = Mathf.FloorToInt(Random.value * 400);
        var posy = Mathf.FloorToInt(Random.value * 300);

        _framebuffer.Buffer.DrawImage(
            _textImg,
            new Rectangle(0, 0, 10, 10),
            new Rectangle(posx, posy, 10, 10)
            );

        _framebuffer.Swap();
        _meshRender.material.mainTexture = _framebuffer.Buffer.NativeTexture;
    }
}
