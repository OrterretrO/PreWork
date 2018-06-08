using UnityEngine;


public class Demo : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Retro.GraphicInterface gi = Retro.GraphicInterface.Create(240, 360);

        var bmp = gi.CreateFrameBuff(50, 50);
        gi.Clear(null, Color.red);

        gi.Clear(bmp, Color.blue);
        gi.DrawLine(bmp, new Vector2(10, 10), new Vector2(45, 10), Color.red);

        gi.DrawImage(null, bmp, new Rect(0, 0, 50, 50), new Rect(100,200, 50,50));

        gi.Swap();
	}

}
