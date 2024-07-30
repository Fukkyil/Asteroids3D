using Godot;
using System;

public partial class ArmoryStationInvSlot : PanelContainer
{
    private Label nameLabel;
    private Label priceLabel;
    private TextureRect weaponTexture;
    
    public void SetParameters(Texture _weaponTexture, string _weaponName, int _weaponPrice){
        nameLabel = GetNode<Label>("MarginContainer/GridContainer/GridContainer/WeaponName");
        nameLabel.Text = _weaponName;

        priceLabel = GetNode<Label>("MarginContainer/GridContainer/WeaponPrice");
        priceLabel.Text = _weaponPrice.ToString();

        weaponTexture = GetNode<TextureRect>("MarginContainer/GridContainer/GridContainer/WeaponTexture");
        weaponTexture.Texture = (Texture2D)_weaponTexture;
    }
}
