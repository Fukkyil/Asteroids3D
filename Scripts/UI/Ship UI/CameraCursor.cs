using Godot;
using System;

public partial class CameraCursor : Control
{
    [Export]
    private Color circleColor = new Color(1,1,1);
    [Export]
    private int segmentNum = 64;
    [Export]
    private int width = 2;
    [Export]
    float radius = 20;
    [Export]
    float finalAngle = 0;
    [Export]
    float startingAngle = 0;
    [Export]
    bool AA = true;

    public override void _Draw()
    {
        DrawArc(Vector2.Zero, radius, startingAngle,finalAngle, segmentNum, circleColor, width, AA);
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

}
