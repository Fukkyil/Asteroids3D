using Godot;
using System;

public partial class ProjectileWeapon : InvWeapon
{
    protected PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/Weapons/Projectiles/GenericProjectile.tscn");

    public override void Shoot(Node3D origin)
    {
        Projectile projectile = (Projectile)projectileScene.Instantiate();
        AddChild(projectile);
    }
}
