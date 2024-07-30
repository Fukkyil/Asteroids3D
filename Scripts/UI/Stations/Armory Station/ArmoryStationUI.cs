using Godot;
using Godot.NativeInterop;
using Structures.Stations;
using System;

public partial class ArmoryStationUI : StationUI
{
private PackedScene invSlotScene = (PackedScene)ResourceLoader.Load("res://Scenes/UI/Stations/Armory Station/ArmoryStationInvSlot.tscn");
private ArmoryStationInvSlot[] inventory;

    public override void _Ready()
    {
    }

    public void AddItem(){

    }

    
}
