using Godot;
using System;

public class Rug : Control
{
    private Color _white => new Color("A39999");
    private Color _grey => new Color("4E4D53");
    private Color _black => new Color("1E1716");
    private Color _red => new Color("C6585F");
    private Color _orange => new Color("C1652A");
    private Color _yellow => new Color("DC915D");

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(0, 0, RectSize), _grey);
        DrawRect(new Rect2(0, 0, 128, 160), _black);

        DrawRect(new Rect2(0, 112, 128, 32), _yellow);

        DrawColoredPolygon(new Vector2[] { new Vector2(0, 0), new Vector2(128, 0), new Vector2(128, 128) }, _black);

        DrawRect(new Rect2(0, 64, 128, 32), _orange);
        DrawRect(new Rect2(0, 16, 128, 32), _red);


        DrawLine(new Vector2(0, 0), new Vector2(128, 128), _black, 8);
        DrawLine(new Vector2(0, 32), new Vector2(128, 128 + 32), _black, 8);
        DrawLine(new Vector2(0, 64), new Vector2(128, 128 + 64), _black, 8);

        DrawColoredPolygon(new Vector2[] { new Vector2(112, 56), new Vector2(128, 72), new Vector2(128, 40) }, _white);
        DrawColoredPolygon(new Vector2[] { new Vector2(112, 128), new Vector2(128, 144), new Vector2(128, 112) }, _white);

        DrawLine(new Vector2(112, 192), new Vector2(128, 176), _white, 8);
        DrawLine(Vector2.Zero, new Vector2(12, 192), _black, 8);
    }
}
