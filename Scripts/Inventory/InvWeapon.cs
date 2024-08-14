using Godot;
using static WeaponSlot;
public partial class InvWeapon : InvItem
{
    public enum WeaponTypeEnum{Plasma, Laser, Kinetic}
    public enum WeaponAmmoEnum{Energy, Canister, Bullet}
    public enum WeaponClassEnum{Vega, Hadar, Alcor, Pollux, Procyon}
    

    protected float weaponDamage;
    
    protected WeaponSlotSizeEnum WeaponFitsSlotSize;
    protected WeaponSlotTypeEnum WeaponFitsSlotType;

    protected WeaponClassEnum WeaponClass;
    protected WeaponAmmoEnum WeaponAmmo;
    protected WeaponTypeEnum WeaponType;
}