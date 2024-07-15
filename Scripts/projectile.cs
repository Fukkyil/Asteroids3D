using Godot;
using System;

public partial class projectile : Area3D
{
    [Signal]
    public delegate void hitEventHandler(Vector3 position);
    [Export]
    public float muzzle_speed = 250f;
    [Export]
    public int playerCollisionLayer = 1;
    [Export]
    public int projectileCollisionLayer = 5;
    public Vector3 rotation = Vector3.Zero;
    Vector3 velocity = Vector3.Zero;


    public override void _Ready()
    {
        SetCollisionMaskValue(playerCollisionLayer, false);
        StartTimer();
    }

    public override void _Process(double delta){
       velocity +=  rotation * muzzle_speed * (float)delta;
       GlobalTransform = new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + velocity * (float)delta);
    }

    private void _on_body_entered(Node body){
        EmitSignal("hit", Transform.Origin);
        QueueFree();
        GD.Print("HIT!" + body);
    }

    private async void StartTimer(){
        await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
        SetCollisionMaskValue(playerCollisionLayer, true);
    }
    

}
