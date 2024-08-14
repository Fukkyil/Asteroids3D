using Godot;
using System;

public partial class WeaponSlot : Node
{
    public enum WeaponSlotSizeEnum{Small, Medium, Big, Special}
    public enum WeaponSlotTypeEnum{Canister, Turret, Platform}
    protected InvWeapon equippedWeapon;
}
