using Godot;
using System;
using System.Collections;

public partial class Projectile : Area3D
{
    protected float projectileSpeed;
    protected Vector3 projectileRotation = Vector3.Zero;
    protected Vector3 projectileVelocity = Vector3.Zero;

    protected MeshInstance3D projectileMeshNode;
    protected CollisionShape3D projectileCollisionShapeNode;
    protected Timer projectileTimerNode;
    public override void _Process(double delta){
       projectileVelocity =  projectileRotation.Normalized() * projectileSpeed;
       GlobalTransform = new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + projectileVelocity * (float)delta);
    }
    private void _on_timer_timeout(){
        QueueFree();
    }


}
