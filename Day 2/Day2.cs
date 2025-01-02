using Godot;
using System;

public class Day2 : Control
{
    private OpenSimplexNoise _noise;
    private Image _image;
    private ImageTexture _texture;
    private ShaderMaterial _shader;

    private float _seetheRate = 5f;
    private float _seethe = 2;

    // private static Color _color1 = new Color("590d22");
    // private static Color _color2 = new Color("800f2f");
    // private static Color _color3 = new Color("a4133c");
    // private static Color _color4 = new Color("c9184a");
    // private static Color _color5 = new Color("ff4d6d");
    // private static Color _color6 = new Color("ff8fa3");
    // private static Color _color7 = new Color("ffb3c1");
    // private static Color _color8 = new Color("ffccd5");
    // private static Color _color9 = new Color("fff0f3");
    private static Color _color1 = new Color("120303");
    private static Color _color2 = new Color("F94144");
    private static Color _color3 = new Color("F3722C");
    private static Color _color4 = new Color("F8961E");
    private static Color _color5 = new Color("F9C74F");
    private static Color _color6 = new Color("90BE6D");
    private static Color _color7 = new Color("43AA8B");
    private static Color _color8 = new Color("577590");
    private static Color _color9 = new Color("F3F5F7");

    private Color[] _colArray = new Color[] { _color1, _color2, _color3, _color4, _color5, _color6, _color7, _color8, _color9 };

    public override void _Ready()
    {
        _noise = new OpenSimplexNoise();

        // values taken from https://docs.godotengine.org/en/3.5/classes/class_opensimplexnoise.html
        _noise.Seed = (int)new RandomNumberGenerator().Randi(); // make random later
        _noise.Octaves = 5;
        _noise.Period = 32;
        _noise.Persistence = .3f;
        _noise.Lacunarity = 2f;

        _image = new Image();
        _image.Create(512, 512, false, Image.Format.Rgb8);
        _texture = new ImageTexture();
        _texture.CreateFromImage(_image);

        CanvasItem item = GetNode<CanvasItem>("ColorRect");
        _shader = (ShaderMaterial)item.Material;
        _shader.SetShaderParam("noise_texture", _texture);
    }

    public override void _Process(float delta)
    {
        _seethe += delta * _seetheRate;

        _image.Lock();
        for (int y = 0; y < 64; y++)
        {
            float threshold = Mathf.RangeLerp(y, 0, 64, -1f, 1f);

            for (int x = 0; x < 64; x++)
            {
                Rect2 rect = new Rect2(x * 8, y * 8, 8, 8);
                float v = _noise.GetNoise3d(x, y + _seethe * 2, _seethe);

                // 0 - 64, 0, 16 32, 48 64
                // stage [0, 1, 2, 3, 4]
                int stage = 4;

                // if (y >= 0 && y < 13)
                //     stage -= 2;
                // if (y >= 13 && y < 26)
                //     stage -= 1;
                // if (y >= 26 && y < 39)
                //     stage += 0;
                // if (y >= 39 && y < 52)
                //     stage += 1;
                // if (y >= 52 && y <= 64)
                //     stage += 2;

                if (v <= -0.4 + threshold)
                    stage -= 4;
                if (v > -0.4 + threshold && v <= -0.3 + threshold)
                    stage -= 3;
                if (v > -0.3 + threshold && v <= -0.2 + threshold)
                    stage -= 2;
                if (v > -0.2 + threshold && v <= -0.1 + threshold)
                    stage -= 1;
                if (v > -0.1 + threshold && v <= 0.1 + threshold)
                    stage += 0;
                if (v > 0.1 + threshold && v <= 0.2 + threshold)
                    stage += 1;
                if (v > 0.2 + threshold && v <= 0.3 + threshold)
                    stage += 2;
                if (v > 0.3 + threshold && v <= 0.4 + threshold)
                    stage += 3;
                if (v > 0.4 + threshold)
                    stage += 4;

                if (stage < 0)
                    stage = 0;
                if (stage > 8)
                    stage = 8;

                _image.FillRect(rect, _colArray[stage]);
            }
        }
        _image.Unlock();
        _texture.SetData(_image);

        // Update();
    }

    public override void _Draw()
    {

    }
}
