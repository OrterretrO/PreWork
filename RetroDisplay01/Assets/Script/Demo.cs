using UnityEngine;
using Retro;


public class Demo : MonoBehaviour
{
    private IRetroGraph m_graph;
    private FrameBuff m_image;


	// Use this for initialization
	void Start ()
    {
        m_graph = TextureGraph.Create(120, 90);

        m_image = m_graph.CreateFrameBuff(10, 10);

        m_graph.Clear(null, Color.red);

        m_graph.Clear(m_image, Color.blue);
        m_graph.DrawLine(m_image, new Vector2(0, 4), new Vector2(9, 4), Color.green);
	}


    void Update()
    {
        Sample1();
        //Sample2();

        m_graph.Swap();
    }


    private void Sample1()
    {
        var posx = Mathf.FloorToInt(Random.value * 110);
        var posy = Mathf.FloorToInt(Random.value * 80);

        m_graph.DrawImage(null, m_image,
            new Rect(0, 0, 10, 10),
            new Rect(posx, posy, 10, 10)
        );
    }

    private void Sample2()
    {
        m_graph.Clear(null, Color.yellow);

        for (var i = 0; i < 100; i++)
        {
            m_graph.SetPixel(null, Mathf.FloorToInt(Random.value * 120), Mathf.FloorToInt(Random.value * 90), Color.blue);
        }
    }
}
