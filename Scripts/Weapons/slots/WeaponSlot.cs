using Godot;
using System;

public partial class WeaponSlot : Node3D
{
    public enum WeaponSlotPriorityEnum{Primary, Secondary, Tertiary}
    public enum WeaponSlotSizeEnum{Small, Medium, Big, Special}
    public enum WeaponSlotTypeEnum{Canister, Turret, Platform}
    protected Spaceship parentShip;
    [Export]
    protected InvWeapon equippedWeapon;
    protected WeaponSlotSizeEnum slotSize;
    protected WeaponSlotTypeEnum slotType;
    protected Timer slotCooldownTimerNode;
    [Export]
    protected WeaponSlotPriorityEnum slotPriority = WeaponSlotPriorityEnum.Primary;

    public override void _Ready()
    {
        CallDeferred(nameof(initializeSlot));
        if(equippedWeapon == null){
            equippedWeapon = new PlasmaRifle();
        }
        slotCooldownTimerNode = GetNode<Timer>("CooldownTimer");
    }
    public void FireSlot(){
        GD.Print("Equipped weapon is: " + equippedWeapon);
        equippedWeapon.Shoot(this);
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
