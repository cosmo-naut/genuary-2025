using Godot;
using System;

public class Day20 : Control
{
    private Color _black = new Color("011627");
    private Color _white = new Color("fdfffc");
    private Color _green = new Color("2ec4b6");
    private Color _red = new Color("e71d36");
    private Color _yellow = new Color("ff9f1c");

    private Color[] _colours = new Color[] { new Color("011627"), new Color("fdfffc"), new Color("2ec4b6"), new Color("e71d36"), new Color("ff9f1c") };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        Vector2 center = Vector2.One * 256;
        int count = 60;
        float segment = Mathf.Tau / count;
        float time = Time.GetTicksMsec() * 0.0001f;

        Vector2[] points = new Vector2[] { center, Vector2.Zero, Vector2.Zero };
        for (int i = 0; i < count; i++)
        {
            points[1] = center + new Vector2(Mathf.Sin(i * segment + time) * 360, Mathf.Cos(i * segment + time) * 360);
            points[2] = center + new Vector2(Mathf.Sin((i + 1) * segment + time) * 360, Mathf.Cos((i + 1) * segment + time) * 360);
            DrawColoredPolygon(points, _colours[i % _colours.Length]);
        }
    }
}
