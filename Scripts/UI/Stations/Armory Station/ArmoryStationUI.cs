using Godot;


public partial class ArmoryStationUI : StationUI
{
    public override void _Ready(){
        slotParent = GetNode<Node>("PanelContainer/MarginContainer/BoxContainer/ItemList/MarginContainer/ScrollContainer/GridContainer");


        selectedPanelNode = GetNode<Container>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/SelectedPanel");
        nullPanelNode = GetNode<Container>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/NullPanel");

        panelTextureNode = GetNode<TextureRect>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/SelectedPanel/BoxContainer/Panel/TextureRect");
        panelDescriptionNode = GetNode<RichTextLabel>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/SelectedPanel/VBoxContainer/RichTextLabel");
        panelTitleNode = GetNode<Label>("PanelContainer/MarginContainer/BoxContainer/ItemPanel/MarginContainer/SelectedPanel/VBoxContainer/Label");
    }
}
