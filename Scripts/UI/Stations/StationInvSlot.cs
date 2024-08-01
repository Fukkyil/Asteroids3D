using Godot;
using System;

public partial class StationInvSlot : Node
{
    protected StationUI parentStation;
    protected Label nameLabel;
    protected Label priceLabel;
    protected TextureRect slotTextureNode;
    protected TextureRect panelTextureNode;


    public Texture slotTexture;

        public void SetParameters(Texture _weaponTexture, string _weaponName, int _weaponPrice){
        nameLabel.Text = _weaponName;
        priceLabel.Text = _weaponPrice.ToString();
        slotTextureNode.Texture = (Texture2D)_weaponTexture;
        panelTextureNode.Texture = (Texture2D)_weaponTexture;
    }

        public void _on_gui_input(InputEvent @event){
        if(@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed){
            parentStation.SlotClicked(this);
        }
    }
}
