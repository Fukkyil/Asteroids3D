using Godot;
using System;

public partial class GenericProjectile : Projectile
{
    public enum ProjectileShapeEnum{Sphere, Box};

    protected ProjectileShapeEnum projectileShape;
    GenericProjectile(){
        projectileMeshNode = GetNode<MeshInstance3D>("MeshInstance3D");
        projectileCollisionShapeNode = GetNode<CollisionShape3D>("CollisionShape3D");
        projectileTimerNode = GetNode<Timer>("Timer");
    }

    public void SetupGenericProjectile(float despawnTime, ProjectileShapeEnum shape, Vector3 size, Material material){
        projectileTimerNode.WaitTime = despawnTime;
        setProjectileMesh(shape, size, material);
        setProjectileCollisionShape(shape, size);
        projectileTimerNode.Start();
    }

    protected void setProjectileMesh(ProjectileShapeEnum shape, Vector3 size, Material material){
        if(shape == ProjectileShapeEnum.Box){
            BoxMesh mesh = new BoxMesh
            {
                Size = size,
                Material = material
            };
            projectileMeshNode.Mesh = mesh;
        }
    }
    protected void setProjectileCollisionShape(ProjectileShapeEnum shape, Vector3 size){
        if(shape == ProjectileShapeEnum.Box){
            BoxShape3D boxShape = new BoxShape3D(){
                Size = size,
            };
            projectileCollisionShapeNode.Shape = boxShape;
        }

    }
}
