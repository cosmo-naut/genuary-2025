using System.Linq;
using Godot;

public class Day3 : Control
{
    [Export] public NoiseTexture Noise;
    private Color[] _colors = new Color[] { new Color("558a78"), new Color("853512"), new Color("ff9500"), new Color("ffb700"), new Color("4a834c") };

    public override void _Process(float delta) => Update();

    public override void _Draw()
    {
        Vector2[] cw = new Vector2[8];
        Vector2[] ccw = new Vector2[8];
        float segment = 1f / 8;
        float time = Time.GetTicksMsec() * 0.0005f;
        for (int i = 0; i < 8; i++)
        {
            cw[i] = new Vector2(Mathf.Sin((i * segment * Mathf.Tau) + time), Mathf.Cos((i * segment * Mathf.Tau) + time));
            ccw[i] = new Vector2(Mathf.Cos((i * segment * Mathf.Tau) + time), Mathf.Sin((i * segment * Mathf.Tau) + time));
        }
        DrawRect(new Rect2(Vector2.Zero, Vector2.One * 512), _colors[4]);
        DrawCircle(Vector2.One * 256, 128, _colors[3]);
        for (int i = 0; i < 8; i++)
            DrawColoredPolygon(cw.Select(vec => vec * (90 + 20 * Noise.Noise.GetNoise2d(24, time)) + new Vector2(256 + ccw[i].x * (180 + 20 * Noise.Noise.GetNoise2d(20, time)), 256 + ccw[i].y * (180 + 20 * Noise.Noise.GetNoise2d(20, time)))).ToArray(), _colors[3]);
        for (int i = 0; i < 8; i++)
            DrawColoredPolygon(ccw.Select(vec => vec * (65 + 20 * Noise.Noise.GetNoise2d(16, time)) + new Vector2(256 + cw[i].x * (150 + 20 * Noise.Noise.GetNoise2d(12, time)), 256 + cw[i].y * (150 + 20 * Noise.Noise.GetNoise2d(12, time)))).ToArray(), _colors[2]);
        for (int i = 0; i < 8; i++)
            DrawColoredPolygon(cw.Select(vec => vec * (40 + 20 * Noise.Noise.GetNoise2d(8, time)) + new Vector2(256 + ccw[i].x * (80 + 20 * Noise.Noise.GetNoise2d(4, time)), 256 + ccw[i].y * (80 + 20 * Noise.Noise.GetNoise2d(4, time)))).ToArray(), _colors[1]);
        DrawColoredPolygon(cw.Select(vec => vec * (50 + 40 * Noise.Noise.GetNoise2d(0, time)) + Vector2.One * 256).ToArray(), _colors[0]);
    }
}
