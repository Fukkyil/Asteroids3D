using Godot;
using System.Collections.Generic;

public partial class SpaceStation : Node3D
{
    public string stationName = "testStation";
    public enum stationFactionEnum{Lunare, test1, test2, test3};
    public enum stationTypeEnum{Mining,Armory,Mechanic,General};
    public stationTypeEnum StationType;
    public stationFactionEnum StationFaction;
    public UIManager uiManager = new UIManager();
    protected bool isShipdocked;
    protected List<InvItem> stationInventory = new List<InvItem>();

    public override void _Process(double delta)
        {
            if(isShipdocked && Input.IsActionJustPressed("ui_back")){
                UndockShip();
            }
        }
    public void DockShip(){
        GD.Print("Ship just docked at " + stationName);
        GD.Print("Station type: " + StationType);
        uiManager.InstantiateUI(StationType, this);
        ParseInventoryToUI();
        isShipdocked = true;
    }
    public void UndockShip(){
        GD.Print("Ship just undocked at " + stationName);
        uiManager.UninstantiateUI();
        isShipdocked = false;
    }

    public void PrintInventory(){
        GD.Print("PrintIventory was called by " + this);
        foreach(InvItem item in stationInventory){
            GD.Print("Itemname: " + item.ItemName);
            GD.Print("Itemprice: " + item.ItemPrice);
            GD.Print("Itemfaction: " + item.ItemFaction);
            GD.Print("Itemtype: " + item.ItemType);
        }
    }

    public void AddItemInInv(InvItem item){
        stationInventory.Add(item);
    }

    public void CreateInvItem(string name, string description, Texture2D texture, int price, InvItem.ItemTypeEnum type, InvItem.ItemFactionEnum faction){
        InvItem item = new InvItem
        {
            ItemName = name,
            ItemDescription = description,
            ItemTexture = texture,
            ItemPrice = price,
            ItemType = type,
            ItemFaction = faction
        };
            stationInventory.Add(item);
    }

    protected void ParseInventoryToUI(){
        GD.Print("Parse UI");
        PrintInventory();
        foreach(InvItem item in stationInventory){
            GD.Print("Item: " + item.ItemName + " Was instantiated!");
            uiManager.stationUI.AddUIItem(item);
        }
    }
    
    protected void ClearUIInventory(){
        GD.Print("Clear UI");
        foreach(InvItem item in stationInventory){
            uiManager.stationUI.RemoveUIItem(item);
        }
    }
}