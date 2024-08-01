using Godot;
using System;

public partial class StationSpawner : Node3D
{
    protected PackedScene stationScene;

    public override void _Ready(){
    stationScene = (PackedScene)GD.Load("res://Scenes/Stations/Armory Station.tscn");

    }
    public void _on_spawn_area_entered(Area3D area){
        GD.Print("Entered!" + area);
    }

    public void _on_despawn_area_entered(Area3D area){
        GD.Print("Entered!" + area);

    }


}
