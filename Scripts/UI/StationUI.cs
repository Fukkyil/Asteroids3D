using Godot;
using Manager.Inventory.Item;
using System;
using System.Collections.Generic;

public partial class StationUI : Node
{
    protected PackedScene invSlotScene;
    protected StationInvSlot selectedSlot;
    protected TextureRect panelTextureNode;
    protected Node slotParent;
    protected Container panelNode;
    public void SlotClicked(StationInvSlot slot){
        if(selectedSlot == slot){
            panelNode.Visible = false;
        }
        else{
            selectedSlot = slot;
            updatePanel(slot.GetReferenceItem());
            panelNode.Visible = true;
        }
    }

    public void AddUIItem(InvItem item){
        StationInvSlot invslot = (StationInvSlot)invSlotScene.Instantiate();
        slotParent.AddChild(invslot);
        invslot.SetInvSlotParameters(item);
        invslot.SetParentStation(this);
    }

    public void RemoveUIItem(InvItem item){
        foreach(Node node in slotParent.GetChildren()){
            if(node is StationInvSlot slot && slot.GetReferenceItem() == item){
                slot.QueueFree();
                break;
            }
        }
    }

    public void SetUISlotScene(PackedScene scene){
        invSlotScene = scene;
    }

    protected void updatePanel(InvItem item){
        panelTextureNode.Texture = (Texture2D)item.ItemTexture;
    }

}
