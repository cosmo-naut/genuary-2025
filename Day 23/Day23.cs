using Godot;
using System;

public class Day23 : Control
{
    private Color[] _colours = new Color[] {
        new Color("fbfbf2"),
        new Color("e5e6e4"),
        new Color("cfd2cd"),
        new Color("a6a2a2"),
        new Color("847577"),
    };

    private Rect2[] _under;
    private Rect2[] _mid;
    private Rect2[] _over;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Generate();
    }

    public void Generate()
    {
        _under = new Rect2[3];
        _mid = new Rect2[4];
        _over = new Rect2[5];

        GD.Randomize();

        for (int i = 0; i < _under.Length; i++)
        {
            _under[i] = new Rect2(
                (float)GD.RandRange(0, 256),
                (float)GD.RandRange(0, 256),
                (float)GD.RandRange(128, 256),
                (float)GD.RandRange(128, 256)
            );
        }
        for (int i = 0; i < _mid.Length; i++)
        {
            _mid[i] = new Rect2(
                (float)GD.RandRange(64, 384),
                (float)GD.RandRange(64, 384),
                (float)GD.RandRange(64, 256),
                (float)GD.RandRange(64, 256)
            );
        }
        for (int i = 0; i < _over.Length; i++)
        {
            _over[i] = new Rect2(
                (float)GD.RandRange(32, 384),
                (float)GD.RandRange(128, 384),
                (float)GD.RandRange(32, 128),
                (float)GD.RandRange(32, 128)
            );
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (Input.IsActionJustPressed("ui_select"))
            Generate();

        for (int i = 0; i < _mid.Length; i++)
        {
            Vector2 pos = _mid[i].Position;
            pos.x = (pos.x + delta * (4 * (i + 1))) % 512;

            _mid[i] = new Rect2(pos, _mid[i].Size);
        }

        for (int i = 0; i < _over.Length; i++)
        {
            Vector2 pos = _over[i].Position;
            pos.y = (pos.y + delta * (6 * (i + 1))) % 512;

            _over[i] = new Rect2(pos, _over[i].Size);
        }

        Update();
    }

    public override void _Draw()
    {
        foreach (Rect2 rect in _under)
        {
            DrawRect(rect, _colours[4]);

        }
        foreach (Rect2 rect in _mid)
        {
            DrawRect(rect, _colours[3]);
            if (rect.Position.x + rect.Size.x > 512)
                DrawRect(new Rect2(rect.Position - Vector2.Right * 512, rect.Size), _colours[3]);
        }
        foreach (Rect2 rect in _over)
        {
            DrawRect(rect, _colours[2]);
            if (rect.Position.y + rect.Size.y > 512)
                DrawRect(new Rect2(rect.Position - Vector2.Down * 512, rect.Size), _colours[2]);
        }
    }
}
