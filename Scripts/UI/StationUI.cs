using Godot;
public partial class StationUI : Node
{
    protected PackedScene invSlotScene;
    protected StationInvSlot selectedSlot;
    protected TextureRect panelTextureNode;
    protected RichTextLabel panelDescriptionNode;
    protected Label panelTitleNode;
    protected Node slotParent;
    protected Container selectedPanelNode;
    protected Container nullPanelNode;
    public void SlotClicked(StationInvSlot slot){
        if(selectedSlot == slot){
            selectedPanelNode.Visible = false;
            nullPanelNode.Visible = true;
            selectedSlot = null;
        }
        else{
            selectedSlot = slot;
            updatePanel(slot.GetReferenceItem());
            selectedPanelNode.Visible = true;
            nullPanelNode.Visible = false;
        }
    }

    public void AddUIItem(InvItem item){
        StationInvSlot invslot = (StationInvSlot)invSlotScene.Instantiate();
        slotParent.AddChild(invslot);
        invslot.SetParentStation(this);
        invslot.SetInvSlotParameters(item);
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
        panelTitleNode.Text = item.ItemName;
        panelDescriptionNode.Text = item.ItemDescription;
    }

}
