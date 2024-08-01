using Godot;
using Manager.Inventory.Item;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class ArmoryStationInvSlot : StationInvSlot
{
    public ArmoryStationInvSlot(){
        nameLabel = GetNode<Label>("MarginContainer/GridContainer/GridContainer/WeaponName");
        priceLabel = GetNode<Label>("MarginContainer/GridContainer/WeaponPrice");
        slotTextureNode = GetNode<TextureRect>("MarginContainer/GridContainer/GridContainer/WeaponTexture");
        panelTextureNode = GetNode<TextureRect>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/BoxContainer/BoxContainer/Panel/TextureRect");
        parentStation = (ArmoryStationUI)GetParent(); 
    }
}
