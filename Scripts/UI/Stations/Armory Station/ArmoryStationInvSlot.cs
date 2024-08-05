using Godot;
using Manager.Inventory.Item;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class ArmoryStationInvSlot : StationInvSlot
{
    protected override void StartupSlot(){
        nameLabel = GetNode<Label>("MarginContainer/GridContainer/GridContainer/WeaponName");
        priceLabel = GetNode<Label>("MarginContainer/GridContainer/WeaponPrice");
        slotTextureNode = GetNode<TextureRect>("MarginContainer/GridContainer/GridContainer/WeaponTexture");
    }
}