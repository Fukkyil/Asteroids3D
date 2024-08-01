using Godot;
using Godot.NativeInterop;
using Structures.Stations;
using System;

public partial class ArmoryStationUI : StationUI
{
    public ArmoryStationUI(){
        invSlotScene = (PackedScene)ResourceLoader.Load("res://Scenes/UI/Stations/Armory Station/ArmoryStationInvSlot.tscn");
        slotParent = GetNode<Node>("PanelContainer/MarginContainer/BoxContainer/ItemList/ScrollContainer/GridContainer");
        panelNode = GetNode<Container>("PanelContainer/MarginContainer/BoxContainer/ItemPanel");
    }
}
