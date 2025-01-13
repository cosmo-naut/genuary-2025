using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Day13 : Control
{
    private Vector2[] _triangle;
    private float _factor;

    private Queue<Vector2> _q1;
    private Queue<Vector2> _q2;
    private Queue<Vector2> _q3;

    private int _queueLength = 350;

    private Color[] _colours = new Color[] { new Color("38322E"), new Color("463f3a"), new Color("8a817c"), new Color("bcb8b1"), new Color("f4f3ee"), new Color("e0afa0") };

    public override void _Ready()
    {
        _triangle = new Vector2[] { new Vector2(0.5f, 0.0f) * 512, new Vector2(1.0f, 0.866f) * 512, new Vector2(0.0f, 0.866f) * 512 };
        _q1 = new Queue<Vector2>();
        _q2 = new Queue<Vector2>();
        _q3 = new Queue<Vector2>();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // foreach (TrianglePoint point in _points)
        //     point.UpdatePosition(delta);

        _factor += delta * 0.1f;
        _factor %= 1.0f;

        Update();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(Vector2.Zero, Vector2.One * 512), _colours[0]);
        DrawColoredPolygon(_triangle, _colours[1]);

        Vector2[] points = LerpTriangle(_triangle, _factor);
        DrawColoredPolygon(points, _colours[2]);
        points = LerpTriangle(points, _factor);
        DrawColoredPolygon(points, _colours[3]);
        points = LerpTriangle(points, _factor);
        DrawColoredPolygon(points, _colours[4]);

        _q1.Enqueue(points[0]);
        _q2.Enqueue(points[1]);
        _q3.Enqueue(points[2]);

        if (_q1.Count >= _queueLength)
            _q1.Dequeue();
        if (_q2.Count >= _queueLength)
            _q2.Dequeue();
        if (_q3.Count >= _queueLength)
            _q3.Dequeue();

        if (_q1.Count > 2)
            DrawPolyline(_q1.ToArray(), _colours[5], 5);
        if (_q2.Count > 2)
            DrawPolyline(_q2.ToArray(), _colours[5], 5);
        if (_q3.Count > 2)
            DrawPolyline(_q3.ToArray(), _colours[5], 5);
    }

    public Vector2[] LerpTriangle(Vector2[] points, float weight)
    {
        Vector2[] output = new Vector2[3];
        for (int i = 0; i < 3; i++)
        {
            int to = (i + 1) % 3;
            output[i] = points[i].LinearInterpolate(points[to], weight);
        }
        return output;
    }
}
