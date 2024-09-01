using Godot;
using System;

public partial class ProjectileWeapon : InvWeapon
{
    protected PackedScene projectileScene = GD.Load<PackedScene>("res://Scenes/Weapons/Projectiles/GenericProjectile.tscn");

    public override void Shoot()
    {
        Projectile projectile = (Projectile)projectileScene.Instantiate();
        AddChild(projectile);
    }

    public void ShootCustomProjectile(){
        Projectile projectile = (Projectile)projectileScene.Instantiate();
        AddChild(projectile);
        //projectile.GlobalPosition =
        //projectile.Rotation = 
    }
}
