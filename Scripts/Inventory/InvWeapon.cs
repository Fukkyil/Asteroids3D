using Godot;
using Manager.Inventory.Item;
using System;
using System.Runtime.CompilerServices;

public partial class InvWeapon : InvItem
{
    public enum WeaponTypeEnum{Plasma, Laser, Kinetic}
    public enum WeaponAmmoEnum{Energy, Canister, Bullet}
    public enum WeaponSlotEnum{Small, Medium, Big, Special}
    public enum WeaponClassEnum{Vega, Hadar, Alcor, Pollux, Procyon}
    public int Damage;
    public int Muzzle_Speed;
    public float ShotCooldown;
    public WeaponClassEnum WeaponClass;
    public WeaponAmmoEnum WeaponAmmo;
    public WeaponSlotEnum WeaponSlot;
    public WeaponTypeEnum WeaponType;

    public InvWeapon(){
        ItemType = ItemTypeEnum.Weapon;
    }
    
    public bool Shoot(){
        return true;
    }
}