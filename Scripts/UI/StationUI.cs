using Godot;
using Manager.Inventory.Item;
using System;

public partial class StationUI : Node
{
    protected PackedScene invSlotScene;
    protected StationInvSlot selectedSlot;
    protected Texture panelTexture;
    protected Node slotParent;
    protected Container panelNode;
    protected InvItem[] stationInventory;

    public void SlotClicked(StationInvSlot slot){
        if(selectedSlot == slot){
            panelNode.Visible = false;
        }
        else{
        selectedSlot = slot;
        updatePanel(selectedSlot.slotTexture);
            panelNode.Visible = true;
        }

    }
    public void AddItem(InvItem item){
        StationInvSlot invslot = (StationInvSlot)invSlotScene.Instantiate();
        invslot.SetParameters(item.ItemTexture, item.ItemName, item.ItemPrice);
        slotParent.AddChild(invslot);
    }

    protected void updatePanel(Texture _panelTexture){
        panelTexture = _panelTexture;
    }


}
