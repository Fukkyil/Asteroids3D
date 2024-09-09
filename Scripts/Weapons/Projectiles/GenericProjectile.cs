using Godot;
using System;

public partial class GenericProjectile : Projectile
{
    public Vector3 projectileSize = new Vector3(0.6f, 6, 0.6f);
    public Color projectileColor = new Color(255,255,0);
    public Color projectileEmissionColor = new Color(255,255, 0);
    public float projectileEmissionMultiplier = 16;
    public float projectileDespawnTime = 15;
    public float projectileMuzzleSpeed = 500;


    protected StandardMaterial3D projectileMaterial = new StandardMaterial3D();

    public override void _Ready()
    {
        _projectileMuzzleSpeed = projectileMuzzleSpeed;
        projectileMeshNode = GetNode<MeshInstance3D>("./MeshInstance3D");
        projectileCollisionShapeNode = GetNode<CollisionShape3D>("./CollisionShape3D");
        projectileDespawnTimerNode = GetNode<Timer>("./DespawnTimer");

        projectileMaterial = new StandardMaterial3D{
            AlbedoColor = projectileColor,
            EmissionEnabled = true,
            Emission = projectileEmissionColor,
            EmissionEnergyMultiplier = projectileEmissionMultiplier,
        };

        projectileMeshNode.Mesh = new BoxMesh{
            Size = projectileSize,
            Material = projectileMaterial,
        };
        

        projectileCollisionShapeNode.Shape = new BoxShape3D{
            Size = projectileSize,
        };

        //projectileMeshNode.RotateZ(90);
        //projectileCollisionShapeNode.RotateZ(90);
        projectileDespawnTimerNode.WaitTime = projectileDespawnTime;
    }
}
