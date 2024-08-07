using Godot;

public partial class ArmoryStationInvSlot : StationInvSlot
{
    protected override void StartupSlot(){
        nameLabel = GetNode<Label>("MarginContainer/GridContainer/GridContainer/WeaponName");
        priceLabel = GetNode<Label>("MarginContainer/GridContainer/PanelContainer/WeaponPrice");
        slotTextureNode = GetNode<TextureRect>("MarginContainer/GridContainer/GridContainer/WeaponTexture");
    }
}