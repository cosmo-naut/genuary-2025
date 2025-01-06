using Godot;
using System;

public class Day7 : Control
{


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Update();
    }

    public override void _Draw()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                float xPos = x * 512 / 8;
                float yPos = y * 512 / 8;
                DrawRect(new Rect2(xPos, yPos, 512 / 8, 512 / 8), (x + y) % 2 == 0 ? Colors.Black : Colors.White);
            }
        }
    }
}
