using Godot;
using Manager.UI;
using Structures.Stations;
using System;

public partial class ArmoryStation : SpaceStation
{
    public ArmoryStation(){
        StationType = stationTypeEnum.Armory;
        stationName = "Test";
    }


}
