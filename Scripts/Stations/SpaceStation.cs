using Godot;
using Manager.Inventory.Item;
using Manager.UI;
using System;

namespace Structures.Stations{
public partial class SpaceStation : Node3D
{
    public string stationName = "undefined";
    public bool stationBuysItems;
    public bool stationSellsItems;
    public enum stationFactionEnum{Lunare, test1, test2, test3};
    public enum stationTypeEnum{Mining,Armory,Mechanic,General};
    public stationTypeEnum StationType;
    public stationFactionEnum StationFaction;
    private UIManager uiManager;
    private InvItem[] StationInventory;
    public void DockShip(){
        GD.Print("Ship just docked at " + stationName);
    }
    public void UndockShip(){
        GD.Print("Ship just undocked at " + stationName);

    }

}}