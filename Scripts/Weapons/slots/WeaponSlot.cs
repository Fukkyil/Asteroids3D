using Godot;
using System;

public partial class WeaponSlot : Node
{
    public enum WeaponSlotPriorityEnum{Primary, Secondary, Tertiary}
    public enum WeaponSlotSizeEnum{Small, Medium, Big, Special}
    public enum WeaponSlotTypeEnum{Canister, Turret, Platform}
    protected Spaceship parentShip;
    [Export]
    protected InvWeapon equippedWeapon = new InvWeapon();
    protected WeaponSlotSizeEnum slotSize;
    protected WeaponSlotTypeEnum slotType;
    [Export]
    protected WeaponSlotPriorityEnum slotPriority = WeaponSlotPriorityEnum.Primary;

    public override void _Ready()
    {
        CallDeferred(nameof(initializeSlot));
    }
    public void FireSlot(){
        equippedWeapon.Shoot();
    }
    public void UnequipWeaponSlot(){
        equippedWeapon = null;
    }
    public void EquipWeaponSlot(InvWeapon weapon){
        equippedWeapon = weapon;
    }
    public WeaponSlotPriorityEnum GetWeaponSlotPriority(){
        return slotPriority;
    }

    protected void initializeSlot(){
        parentShip = GetParent<Spaceship>();
        parentShip.slotManager.AddWeaponSlot(this); 
    }

}
