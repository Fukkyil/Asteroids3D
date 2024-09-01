using Godot;
using System;

public partial class SmallTurretSlot : WeaponSlot
{
    public SmallTurretSlot(){
        slotSize = WeaponSlotSizeEnum.Small;
        slotType = WeaponSlotTypeEnum.Turret;
    }
}
