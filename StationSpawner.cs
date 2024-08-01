using Godot;
using System;

public partial class StationSpawner : Node3D
{
    protected PackedScene stationScene;
    protected ArmoryStation sceneInstance;
    protected bool isStationSpawned = false;

    public override void _Ready(){
    stationScene = (PackedScene)GD.Load("res://Scenes/Stations/Armory Station.tscn");
    sceneInstance = (ArmoryStation)stationScene.Instantiate();
    }
    public void _on_spawn_area_entered(Area3D area){
        GD.Print("Spawnarea Entered!" + area);
        if(!isStationSpawned){
            AddChild(sceneInstance);
            isStationSpawned = true;
        }
    }

    public void _on_despawn_area_entered(Area3D area){
        GD.Print("Despawnarea Entered!" + area);
        isStationSpawned = false;
        sceneInstance.QueueFree();
        
    }

    public void _on_changetimer_timeout(){
        GD.Print("Timeout!");
    }


}
