using Godot;
using System;
using System.Linq;

public class Day6 : Control
{
    private OpenSimplexNoise _noise;
    private Color _sky = new Color("F4F5F6");
    private Color _horizon = new Color("9CAFB7");
    private Color _dry = new Color("F6CA83");
    private Color _dirt = new Color("82735c");
    private Color _grass1 = new Color("D0D38F");
    private Color _grass2 = new Color("ADB993");
    private Color _grass3 = new Color("949D6A");
    private Color _trunk = new Color("fceff9");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _noise = new OpenSimplexNoise();
        GD.Randomize();
        _noise.Seed = (int)GD.Randi();
        _noise.Octaves = 4;
        _noise.Persistence = 0.6f;
        _noise.Period = 16;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Vector2.One * 512), _sky);

        Vector2[] horizon = new Vector2[32];
        float step = 512f/31f;
        for (int i = 0; i < 32; i++)
        {
            horizon[i] = new Vector2(step * i, 170 + 20 * _noise.GetNoise1d(i));
        }

        DrawPolyline(horizon, _horizon, 32);

        Vector2[] hill = new Vector2[]
        {
            new Vector2(0, 175),
            new Vector2(512, 165),
            new Vector2(512,512),
            new Vector2(0,512),
        };

        DrawColoredPolygon(hill, _dry);
        DrawColoredPolygon(hill.Select(v => v + Vector2.Down * 20).ToArray(), _dirt);

        int treeCount = 400;
        Vector2[] treePos = new Vector2[treeCount];

        for (int i = 0; i < treeCount; i++)
        {
            treePos[i] = new Vector2(GD.Randf() * 512, 180 + GD.Randf() * 190);
            DrawTreeTrunk(treePos[i]);
        }

        for (int i = 0; i < treeCount; i++)
        {
            DrawTreeTop(treePos[i]);
        }

        DrawRect(new Rect2(Vector2.Down * 340, Vector2.One * 512), _grass1);

        DrawLine(new Vector2(0, 340), new Vector2(512, 340), _dry, 4);
        step = 512f/4;
        DrawLine(new Vector2(step, 340), new Vector2(step, 240), _dry, 8);
        DrawLine(new Vector2(step * 2, 340), new Vector2(step * 2, 240), _dry, 8);
        DrawLine(new Vector2(step * 3, 340), new Vector2(step * 3, 240), _dry, 8);
    }

    public Color GetTreeColour()
    {
        float roll = GD.Randf();
        if (roll < 0.33f)
            return _grass1;
        if (roll < 0.66f)
            return _grass2;
        return _grass3;
    } 

    public void DrawTreeTrunk(Vector2 pos)
    {
        Vector2 treetop = pos + Vector2.Up * 25;
        DrawLine(pos, treetop, _trunk, 4);
    }

    public void DrawTreeTop(Vector2 pos)
    {
        Vector2 treetop = pos + Vector2.Up * (25 + 10 * GD.Randf());
        Color colour = GetTreeColour();
        
        DrawLine(treetop + new Vector2(-15, 0), treetop + new Vector2(15, 0), colour, 2);
        DrawLine(treetop + new Vector2(-12, -9), treetop + new Vector2(12, 9), colour, 2);
        DrawLine(treetop + new Vector2(-12, 9), treetop + new Vector2(12, -9), colour, 2);
    }
}
