using Godot;
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
        GD.Print("Where??");
        nameLabel.Text = item.ItemName;
        GD.Print("Maybe here?");
        priceLabel.Text = item.ItemPrice.ToString();
        GD.Print("here?");
        slotTextureNode.Texture = item.ItemTexture;
        GD.Print("Is this slot initialized? " + isSlotInitialized);
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
