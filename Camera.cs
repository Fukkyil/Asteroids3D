using Godot;
using System;
using System.Net.Http.Headers;

public partial class Camera : Node3D
{
    [Export]
    public double interpolation_speed = 15.0;
    [Export]
    public Node3D target; 
    [Export]
    public Vector3 offset = new Vector3(0,12,-12);

    public override void _Process(double delta)
    {
        if (target != null){
            Transform3D target_xform = target.GlobalTransform.TranslatedLocal(offset);
            GlobalTransform = GlobalTransform.InterpolateWith(target_xform, (float)interpolation_speed * (float)delta);

            LookAt(target.GlobalTransform.Origin, target.Transform.Basis.Y);
        }
    }
}
