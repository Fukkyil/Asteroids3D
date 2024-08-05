using Godot;
using Manager.Inventory.Item;
using System;

public partial class StationInvSlot : Node
{
    protected StationUI parentStation;
    protected Label nameLabel;
    protected Label priceLabel;
    protected TextureRect slotTextureNode;
    protected InvItem referenceItem = new InvItem();
    protected bool isSlotInitialized = false;

    public Texture slotTexture;

    public override void _Ready()
    {
        StartupSlot();
        isSlotInitialized = true;
    }

    public void SetInvSlotParameters(InvItem item){
        referenceItem = item;
        nameLabel.Text = item.ItemName;
        priceLabel.Text = item.ItemPrice.ToString();
        slotTextureNode.Texture = item.ItemTexture;
    }

    public void SetParentStation(StationUI parent){
        parentStation = parent;
    }
    
    public InvItem GetReferenceItem(){
        return referenceItem;
    }

    protected void _on_gui_input(InputEvent @event){
        if(@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed){
            parentStation.SlotClicked(this);
        }
    }

    protected virtual void StartupSlot(){

    }
    
}
