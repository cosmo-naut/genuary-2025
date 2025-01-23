using Godot;
using System;

public class Day24 : Control
{
    private Color[] _colours = new Color[] {
        new Color("ffd60a"),
        new Color("ffc300"),
        new Color("003566"),
        new Color("001d3d"),
        new Color("000814"),
    };
    private Color[] _soul1 = new Color[] {
        new Color("780000"),
        new Color("c1121f"),
    };
    private Color[] _soul2 = new Color[] {
        new Color("003049"),
        new Color("669bbc"),
    };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        for (int i = 0; i < 30; i++)
        {
            Color col = _soul1[0].LinearInterpolate(_soul1[1], (1 / 30f) * i);
            GD.Print((1 / 30f) * i);
            GD.Print(col.ToHtml(false));
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        Vector2 center = Vector2.One * 256;
        DrawRect(new Rect2(0, 0, 512, 512), _colours[4]);
        DrawCircle(center, 256, _colours[3]);

        float time = Time.GetTicksMsec() * 0.0002f;
        float size = 160;
        int length = 60;

        for (int i = 0; i < length; i++)
        {
            float delta = time + i * 0.03f;
            Vector2 pos = new Vector2(
                Mathf.Sin(Mathf.Cos(delta) * Mathf.Tau),
                Mathf.Cos(Mathf.Sin(delta) * Mathf.Tau)
            ) * size + center;

            Vector2 pos2 = new Vector2(
                Mathf.Sin(Mathf.Cos(delta + Mathf.Pi) * Mathf.Tau),
                -Mathf.Cos(Mathf.Sin(delta + Mathf.Pi) * Mathf.Tau)
            ) * size + center;

            Color col = _soul1[0].LinearInterpolate(_soul1[1], 1.0f / length * i);
            Color col2 = _soul2[0].LinearInterpolate(_soul2[1], 1.0f / length * i);

            DrawLine(pos, pos2, new Color("fdf0d5"), 2);

            DrawCircle(pos, 12 + i * 0.2f, col);
            DrawCircle(pos2, 12 + i * 0.2f, col2);

        }
    }
}
