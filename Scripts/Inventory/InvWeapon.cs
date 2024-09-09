using Godot;
using static WeaponSlot;
public partial class InvWeapon : InvItem
{
    public enum WeaponTypeEnum{Plasma, Laser, Kinetic}
    public enum WeaponAmmoEnum{Energy, Canister, Bullet}
    public enum WeaponClassEnum{Vega, Hadar, Alcor, Pollux, Procyon}
    protected WeaponSlotSizeEnum WeaponFitsSlotSize;
    protected WeaponSlotTypeEnum WeaponFitsSlotType;
    protected WeaponClassEnum WeaponClass;
    protected WeaponAmmoEnum WeaponAmmo;
    protected WeaponTypeEnum WeaponType;
    public virtual void Shoot(Node3D origin){
        GD.Print("Shoot was called from the base InvWeapon class or in an unimplemented child class!!");
    }

    protected void onHit(){
        GD.Print("onHit was called from the base InvWeapon class or in an unimplemented child class!!");
    }

    protected void onDespawn(){
        GD.Print("onDespawn was called from the base InvWeapon class or in an unimplemented child class!!");
    }

    protected void dealDamage(){
        GD.Print("dealDamage was called from the base InvWeapon class or in an unimplemented child class!!");
    }
}