using Godot;
using System.Collections.Generic;
using Structures.Stations;

namespace Manager.UI{
public partial class UIManager : Node
{
    public StationUI stationUI;
    protected Node stationUIParent;
        private Dictionary<SpaceStation.stationTypeEnum, string>  uiScenes = new Dictionary<SpaceStation.stationTypeEnum, string>{
        { SpaceStation.stationTypeEnum.Mining, ""},
        { SpaceStation.stationTypeEnum.Armory, "res://Scenes/UI/Stations/Armory Station/ArmoryStationUI.tscn"},
        { SpaceStation.stationTypeEnum.Mechanic, ""},
        { SpaceStation.stationTypeEnum.General, ""}
    };

    public bool InstantiateUI(SpaceStation.stationTypeEnum UIType, Node root){
        GD.Print("InstantiateUI!");
        if(stationUI != null){
            GD.Print("StationUI is not null?");
            return false;
        }

        string uiPath;
       if(uiScenes.TryGetValue(UIType, out uiPath)){
             PackedScene uiScene = GD.Load<PackedScene>(uiPath);

             if(uiScene != null){
                stationUI = (StationUI)uiScene.Instantiate();
                root.AddChild(stationUI);
                AutoloadGeneral.Instance.isUIOpen = true;
                return true;
            }
        }
       return false;
    }

    public bool UninstantiateUI(){
        if(stationUI == null){
            return false;
        }
        else{
            stationUI.QueueFree();
            AutoloadGeneral.Instance.isUIOpen = false;
            stationUI = null;
            return true;
        }
    }
}}
