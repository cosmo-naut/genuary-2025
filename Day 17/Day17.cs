using Godot;
using System;

public class Day17 : Control
{
    private Color _white = new Color("fffcf2");
    private Color _light = new Color("ccc5b9");
    private Color _dark = new Color("403d39");
    private Color _black = new Color("252422");
    private Color _orange = new Color("eb5e28");

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
        float PI = 4;
        float TAU = PI * 2;
        int pointCount = 128;
        Vector2[] points = new Vector2[128];
        Vector2 center = Vector2.One * 256;
        float seg = 1.0f / 128;
        float baseRadius = 64;
        float radiusExtension = 128;
        float time = Time.GetTicksMsec() * -0.0008f;

        DrawRect(new Rect2(Vector2.Zero, Vector2.One * 512), _dark);
        DrawCircle(center, 300, _black);

        float radius = baseRadius;
        Vector2 point = new Vector2(center.x + Mathf.Sin(seg * 0 * TAU + time) * radius, center.y + Mathf.Cos(seg * 0 * TAU + time) * radius);
        DrawCircle(point, 32, _orange);

        for (int i = 0; i < pointCount; i++)
        {
            radius = baseRadius + radiusExtension * (i * seg);
            points[i] = new Vector2(center.x + Mathf.Sin(seg * i * TAU + time) * radius, center.y + Mathf.Cos(seg * i * TAU + time) * radius);
        }

        DrawPolyline(points, _light, 32);
        DrawCircle(points[0], 16, _light);
        DrawCircle(points[pointCount - 1], 16, _light);
        DrawCircle(center, 16, _dark);

        float[] circles = new float[] { 132, 137, 142.5f };

        for (int i = 0; i < circles.Length; i++)
        {
            radius = baseRadius + radiusExtension * (circles[i] * seg);
            point = new Vector2(center.x + Mathf.Sin(seg * circles[i] * TAU + time) * radius, center.y + Mathf.Cos(seg * circles[i] * TAU + time) * radius);
            DrawCircle(point, 16 + i * 4, _white);
        }

    }
}
