using Godot;
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
    private UIManager uiManager;
    private stationTypeEnum _StationType;


    public bool InstantiateUI(stationTypeEnum type){

        if(uiManager == null){
            uiManager = new UIManager();
        }
        uiManager.InstantiateUI(type);
        return uiManager.InstantiateUI(type);
    }

    public void DockShip(){
        GD.Print("Ship just docked at " + stationName);
    }
    public void UndockShip(){
        GD.Print("Ship just undocked at " + stationName);

    }

}}