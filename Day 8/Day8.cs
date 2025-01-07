using Godot;
using System;

public class Day8 : Control
{
    [Export] public Texture Texture;
    private Color[] _colours = new Color[] { new Color("05668d"), new Color("028090"), new Color("00a896"), new Color("02c39a"), new Color("f0f3bd") };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override void _Draw()
    {
        for (int i = 0; i < 1000000; i++)
        {
            Vector2 pos = new Vector2(GD.Randf() * 512, GD.Randf() * 512);
            Color col = _colours[Mathf.FloorToInt(GD.Randf() * 5)];
            DrawTexture(Texture, pos, col);
            // DrawCircle(pos, 20, col);
        }
    }
}
