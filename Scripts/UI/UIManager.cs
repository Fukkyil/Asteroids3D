using Godot;
using System.Collections.Generic;
using Structures.Stations;

namespace Manager.UI{
public partial class UIManager : Node
{
    private StationUI stationUI;
    private Dictionary<SpaceStation.stationTypeEnum, string>  uiScenes = new Dictionary<SpaceStation.stationTypeEnum, string>{
        { SpaceStation.stationTypeEnum.Mining, ""},
        { SpaceStation.stationTypeEnum.Armory, ""},
        { SpaceStation.stationTypeEnum.Mechanic, ""},
        { SpaceStation.stationTypeEnum.General, ""}
    };

    public bool InstantiateUI(SpaceStation.stationTypeEnum UIType){

        if(stationUI != null){
            return false;
        }

        string uiPath;
       if(uiScenes.TryGetValue(UIType, out uiPath)){
             PackedScene uiScene = GD.Load<PackedScene>(uiPath);

             if(uiScene != null){
                stationUI = (StationUI)uiScene.Instantiate();
                return true;
            }
        }
       return false;
    }
}
}
