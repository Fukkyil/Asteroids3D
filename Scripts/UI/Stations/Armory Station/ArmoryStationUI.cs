using Godot;
using Godot.NativeInterop;
using Structures.Stations;
using System;

public partial class ArmoryStationUI : StationUI
{
    public override void _Ready(){
        panelTextureNode = GetNode<TextureRect>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/BoxContainer/BoxContainer/Panel/TextureRect");
        slotParent = GetNode<Node>("PanelContainer/MarginContainer/BoxContainer/ItemList/ScrollContainer/GridContainer");
        panelNode = GetNode<Container>("PanelContainer/MarginContainer/BoxContainer/ItemPanel");
    }
}
