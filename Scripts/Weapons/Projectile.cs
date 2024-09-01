using Godot;
using System;

public partial class Projectile1 : Area3D
{
    [Signal]
    public delegate void hitEventHandler(Vector3 position);
    [Export]
    public float muzzle_speed = 600f;
    [Export]
    public int playerCollisionLayer = 1;
    [Export]
    public float timeToDespawn = 1;
    public bool canHitShips = true;
    public int shipDamage = 20;
    public bool canHitSelf = true;
    public int selfDamage = 20;
    public bool canHitAsteroids = true;
    public int asteroidDamage = 20;
    public int asteroidSplitFactor = 2;
    public Vector3 rotation = Vector3.Zero;
    public Vector3 velocity = Vector3.Zero;


    public override void _Ready()
    {
        SetCollisionMaskValue(playerCollisionLayer, false);
        StartTimer();
    }
    
    public override void _Process(double delta){
       velocity =  rotation.Normalized() * muzzle_speed;
       GlobalTransform = new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + velocity * (float)delta);
    }

    private void _on_body_entered(Node body){
        EmitSignal("hit", Transform.Origin);
        if(body is ShipController && canHitSelf){
            QueueFree();
        }
        if(body is Asteroid && canHitAsteroids){
            Asteroid asteroid = (Asteroid)body;
            asteroid.takeDamage(asteroidDamage, asteroidSplitFactor, GlobalTransform.Origin, GlobalTransform.Basis);
            QueueFree();
        }
        else{
            GD.Print("You hit something else... did I forget to set it's type?");
            QueueFree();
        }
    }

    private async void StartTimer(){
        await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
        SetCollisionMaskValue(playerCollisionLayer, true);
    }

    private void _on_timer_timeout(){
        QueueFree();
    }
}
