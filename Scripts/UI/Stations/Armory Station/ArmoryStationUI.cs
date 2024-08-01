using Godot;
using Godot.NativeInterop;
using Structures.Stations;
using System;

public partial class ArmoryStationUI : StationUI
{
    public override void _Ready(){
        invSlotScene = (PackedScene)ResourceLoader.Load("res://Scenes/UI/Stations/Armory Station/ArmoryStationInvSlot.tscn");
        slotParent = GetNode<Node>("PanelContainer/MarginContainer/BoxContainer/ItemList/ScrollContainer/GridContainer");
        panelNode = GetNode<Container>("PanelContainer/MarginContainer/BoxContainer/ItemPanel");
        SetUIParameters(invSlotScene, slotParent, panelNode);
    }
}
