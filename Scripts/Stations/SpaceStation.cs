using Godot;
using Manager.Inventory.Item;
using Manager.UI;
using System;

namespace Structures.Stations{
public partial class SpaceStation : Node3D
{
    public string stationName = "testStation";
    public enum stationFactionEnum{Lunare, test1, test2, test3};
    public enum stationTypeEnum{Mining,Armory,Mechanic,General};
    public stationTypeEnum StationType;
    public stationFactionEnum StationFaction;
    protected bool isShipdocked;
    protected UIManager uiManager = new UIManager();
    protected InvItem[] stationInventory;

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
        isShipdocked = true;
    }
    public void UndockShip(){
        GD.Print("Ship just undocked at " + stationName);
        uiManager.UninstantiateUI();
        isShipdocked = false;
    }

    public void AddInvItem(){

    }

    public void CreateInvItem(){
        
    }
}}