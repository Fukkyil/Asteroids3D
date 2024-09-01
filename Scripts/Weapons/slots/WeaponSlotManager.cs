using Godot;
using System;
using System.Collections.Generic;

public partial class WeaponSlotManager : Node
{
    public Spaceship ParentSpaceship;
    protected List <WeaponSlot> primaryWeaponSlots = new List<WeaponSlot>();
    protected List <WeaponSlot> secondaryWeaponSlots = new List<WeaponSlot>();
    protected List <WeaponSlot> tertiaryWeaponSlots = new List<WeaponSlot>();


    public void AddWeaponSlot(WeaponSlot slot){
        weaponSlotPriorityCheck(slot);
    }
    public void FirePrimaryWeapons(){
        foreach(WeaponSlot slot in primaryWeaponSlots){
            slot.FireSlot();
        }
    }

    public void FireSecondaryWeapons(){
       foreach(WeaponSlot slot in secondaryWeaponSlots){
            slot.FireSlot();
        }
    }

    public void FireTertiaryWeapons(){
       foreach(WeaponSlot slot in tertiaryWeaponSlots){
            slot.FireSlot();
        }
    }

    
    protected void weaponSlotPriorityCheck(WeaponSlot slot){
        if(ParentSpaceship == null){
            GD.Print("Parent ship is null!");
        }
        if(slot.GetWeaponSlotPriority() == WeaponSlot.WeaponSlotPriorityEnum.Primary){
            if(primaryWeaponSlots.Count <= ParentSpaceship.PrimaryWeaponCount){
                primaryWeaponSlots.Add(slot);
                GD.Print(ParentSpaceship + "Has added a primary WeaponSlot!");
            }
            else{
                GD.Print(ParentSpaceship + "Just tried to add more primary weapons than it has permission to!");
            }
        }

        if(slot.GetWeaponSlotPriority() == WeaponSlot.WeaponSlotPriorityEnum.Secondary){
            if(secondaryWeaponSlots.Count <= ParentSpaceship.SecondaryWeaponCount){
                secondaryWeaponSlots.Add(slot);
                GD.Print(ParentSpaceship + "Has added a secondary WeaponSlot!");
            }
            else{
                GD.Print(ParentSpaceship + "Just tried to add more secondary weapons than it has permission to!");
            }
        }

        if(slot.GetWeaponSlotPriority() == WeaponSlot.WeaponSlotPriorityEnum.Tertiary){
            if(tertiaryWeaponSlots.Count <= ParentSpaceship.TertiaryWeaponCount){
                tertiaryWeaponSlots.Add(slot);
                GD.Print(ParentSpaceship + "Has added a tertiary WeaponSlot!");
            }
            else{
                GD.Print(ParentSpaceship + "Just tried to add more tertiary weapons than it has permission to!");
            }
        }
    }
}
