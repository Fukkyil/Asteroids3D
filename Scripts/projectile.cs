using Godot;
using System;

public partial class projectile : Area3D
{
    [Signal]
    public delegate void hitEventHandler(Vector3 position);
    [Export]
    public float muzzle_speed = 50f;
    public Vector3 rotation = Vector3.Zero;
    Vector3 velocity = Vector3.Zero;


    public override void _Process(double delta){
       velocity +=  rotation * muzzle_speed * (float)delta;
       GlobalTransform = new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + velocity * (float)delta);
        


    }

    private void _on_Body_entered(Node body){
        EmitSignal("hitEventHandler", Transform.Origin);
        QueueFree();
    }
    

}
