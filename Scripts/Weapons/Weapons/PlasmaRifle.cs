using Godot;

public partial class PlasmaRifle : ProjectileWeapon{
    public PlasmaRifle(){
        WeaponType = WeaponTypeEnum.Plasma;
        WeaponAmmo = WeaponAmmoEnum.Energy;
        WeaponClass = WeaponClassEnum.Vega;
        WeaponFitsSlotSize = WeaponSlot.WeaponSlotSizeEnum.Small;
        WeaponFitsSlotType = WeaponSlot.WeaponSlotTypeEnum.Turret;

        projectileScene = GD.Load<PackedScene>("res://Scenes/Weapons/Projectiles/GenericProjectile.tscn");
    }

    public override void Shoot(Node3D origin)
    {
        GD.Print("Shooting plasma rifle!");
        GenericProjectile bullet = (GenericProjectile)projectileScene.Instantiate();
        bullet.projectileColor = new Color(0,255,0);
        bullet.projectileEmissionColor = new Color(0,255,0);
        origin.GetTree().Root.GetNode("Main").AddChild(bullet);
        bullet.GlobalTransform = origin.GlobalTransform;
    }
}