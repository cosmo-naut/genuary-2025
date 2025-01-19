using Godot;
using System;

public class Day20_2 : Control
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        float segment = Mathf.Tau / 5;
        for (int i = 0; i < _colours.Length; i++)
        {
            float radius = Mathf.Sin((Time.GetTicksMsec() * 0.0002f) + i * segment);
            DrawArc(Vector2.One * 256, radius * 128, 0, Mathf.Tau, 64, _colours[i], 16);
        }
    }
}
