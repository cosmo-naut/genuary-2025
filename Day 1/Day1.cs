using Godot;
using System;

public class Day1 : Control
{
    private static int _rows = 12;
    private static int _cols = 32;
    private static int _xStep = Genuary.WIDTH / _cols;
    private static int _yStep = Genuary.HEIGHT / _rows;
    private static int _xOffset = 32;
    private static int _yOffset = -62;
    private static float _seetheRate = 1f;

    private OpenSimplexNoise _noise;
    private float _seethe = 2;


    private float _low = 1;
    private float _high = -1;

    private Color _color1 = new Color("03045e");
    private Color _color2 = new Color("3d348b");
    private Color _color3 = new Color("f7b801");
    private Color _color4 = new Color("f18701");
    private Color _color5 = new Color("f35b04");

    public override void _Ready()
    {
        _noise = new OpenSimplexNoise();

        // values taken from https://docs.godotengine.org/en/3.5/classes/class_opensimplexnoise.html
        _noise.Seed = (int)new RandomNumberGenerator().Randi(); // make random later
        _noise.Octaves = 5;
        _noise.Period = 32;
        _noise.Persistence = .3f;
        _noise.Lacunarity = 2f;

        Image noiseImage = _noise.GetImage(512, 512);
        ImageTexture texture = new ImageTexture();
        texture.CreateFromImage(noiseImage);
        GetNode<TextureRect>("TextureRect").Texture = texture;
    }

    public override void _Process(float delta)
    {
        _seethe += delta * _seetheRate;

        float value = _noise.GetNoise2d(1f, _seethe);
        value = (value + 1) / 2;

        Update();
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(0, 0, 512, 512), _color1);

        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _cols; x++)
            {
                float xPos = x * _xStep + _xOffset;
                float yPos = y * _yStep + _yOffset + x * 8;
                yPos += x % 2 == 0 ? 2 : 0;
                Vector2 pos = new Vector2(xPos, yPos);

                // gives between [-1, 1]
                float noiseValue = _noise.GetNoise3d(x * 12, y * 12, _seethe);
                // normalise to [0, 1]
                noiseValue = (noiseValue + 1) / 2;

                // GD.Print(noiseValue);
                Color color = _color2;
                if (noiseValue > 0.3)
                    color = _color2;
                if (noiseValue > 0.4)
                    color = _color3;
                if (noiseValue > 0.5)
                    color = _color4;
                if (noiseValue > 0.6)
                    color = _color5;

                Vector2 from = new Vector2(pos.x - 120f * noiseValue, pos.y);
                Vector2 to = new Vector2(pos.x + 120f * noiseValue, pos.y);
                DrawLine(from, to, color, 2);

                // DrawCircle(pos, 30 * noiseValue, Colors.White.LinearInterpolate(Colors.Black, noiseValue));
            }
        }
    }
}
