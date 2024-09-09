using Godot;
using System;
using System.Collections;

public partial class Projectile : Area3D
{
    protected float _projectileMuzzleSpeed;
    protected Vector3 projectileVelocity = Vector3.Zero;

    protected MeshInstance3D projectileMeshNode;
    protected CollisionShape3D projectileCollisionShapeNode;
    protected Timer projectileDespawnTimerNode;
    public override void _Process(double delta){
        projectileVelocity =  -GlobalTransform.Basis.Z * _projectileMuzzleSpeed;
        GlobalTransform = new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + projectileVelocity * (float)delta);
    }
    private void _on_timer_timeout(){
        QueueFree();
    }


}
