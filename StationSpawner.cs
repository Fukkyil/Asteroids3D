using Godot;

public partial class StationSpawner : Node3D
{
    protected PackedScene stationScene;
    protected ArmoryStation sceneInstance;
    protected bool isStationSpawned = false;

    public override void _Ready(){
    stationScene = (PackedScene)GD.Load("res://Scenes/Stations/Armory Station.tscn");
    }
    public void _on_spawn_area_entered(Area3D area){
        GD.Print("Spawnarea Entered!" + area + isStationSpawned);
        if(area is Projectile){
            GD.Print("Projectile!!!!!");
            if(isStationSpawned){
                GD.Print("Creating items");
                sceneInstance.CreateInvItem("Person", "This is a person, you may use them in your crew!", new Texture2D(), 200, InvItem.ItemTypeEnum.Biological, InvItem.ItemFactionEnum.General);
                sceneInstance.CreateInvItem("Magnesium asteroid piece", "This piece of an asteroid is rich in magnesium and can be used to create strong metal alloys", new Texture2D(), 120, InvItem.ItemTypeEnum.Ore, InvItem.ItemFactionEnum.General);
                sceneInstance.CreateInvItem("Plant sample", "This is a random plant sample from an unknown planet", new Texture2D(), 50, InvItem.ItemTypeEnum.Biological, InvItem.ItemFactionEnum.General);
                sceneInstance.CreateInvItem("Ritualistic dagger", "This beautifully carved obsidian dagger is oftentimes used in rituals of human sacrifice", new Texture2D(), 500, InvItem.ItemTypeEnum.Weapon, InvItem.ItemFactionEnum.Cultists);
                sceneInstance.CreateInvItem("Flask of \"ferrofluid AE-X10\"", "This fluid is theorized to be used by the {Faction without a name yet, fuck} to mass produce their high tech suits and self-repairing ships.", new Texture2D(), 1200, InvItem.ItemTypeEnum.Consumable, InvItem.ItemFactionEnum.Professionals);
                sceneInstance.CreateInvItem("Improvised disposable nuclear reactor", "Scrapper tech used as an emergency power source or as a one-shot weapon in dire situations, extremely unstable and prone to meltdowns.", new Texture2D(), 1000, InvItem.ItemTypeEnum.Disposable, InvItem.ItemFactionEnum.Scrappers);
                GD.Print("Items created");
            }
            if(!isStationSpawned){
                sceneInstance = (ArmoryStation)stationScene.Instantiate();
                AddChild(sceneInstance);
                isStationSpawned = true;
            }
        }

    }

    public void _on_despawn_area_entered(Area3D area){
        GD.Print("Despawnarea Entered!" + area);
        if(area is Projectile){
            if(isStationSpawned){
                isStationSpawned = false;
                sceneInstance.QueueFree();
            }
        }
    }

    public void _on_changetimer_timeout(){
        GD.Print("Changetimer Timeout");
    }


}
