using Godot;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class day5 : Control
{
    private OpenSimplexNoise _noise;
    private Color _background = new Color("000000");
    private Color _lines = new Color("ffffff");

    private float _height = 200f;
    private float _width = 300f;
    private float _lw = 4;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _noise = new OpenSimplexNoise();
        _noise.Seed = (int)GD.Randi();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Vector2.One * 512), _background);

        Vector2 center = Vector2.One * 256;
        Vector2 roofPoint = center + Vector2.Up * _height;

        Vector2[] topPoints = new Vector2[4];
        Vector2[] botPoints = new Vector2[4];

        float time = Time.GetTicksMsec() * 0.0005f;

        for (int i = 0; i < 4;  i++)
        {
            Vector2 pos = new Vector2(Mathf.Sin(i * 0.25f * Mathf.Tau + time) * _width / 2, Mathf.Cos(i * 0.25f * Mathf.Tau + time) * _height / 4) + center;

            topPoints[i] = pos + Vector2.Up * _height/2;
            botPoints[i] = pos + Vector2.Down * _height/2;
        }

        for (int i = 0; i < 4; i++)
        {
            DrawLine(topPoints[i], botPoints[i], _lines, _lw * 2);
            DrawCircle(topPoints[i], _lw, _lines);
            DrawCircle(botPoints[i], _lw, _lines);

            DrawLine(topPoints[i], topPoints[(i + 1) % 4], _lines, _lw * 2);
            DrawLine(botPoints[i], botPoints[(i + 1) % 4], _lines, _lw * 2);

            DrawCircle(roofPoint, _lw, _lines);

            DrawLine(topPoints[i], roofPoint, _lines, _lw * 2);
        }
    }
}
