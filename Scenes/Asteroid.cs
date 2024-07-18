using Godot;
using System;

public partial class Asteroid : RigidBody3D
{  
[Export]
public int maxHealth = 100;
[Export]
public int minAsteroidsOnSplit = 3;
[Export]
public int maxAsteroidsOnSplit = 13;
[Export]
public float minAsteroidSpreadSpeed = 200;
[Export]
public float maxAsteroidSpreadSpeed = 500;

Node mainScene;
private PackedScene asteroidScene = ResourceLoader.Load<PackedScene>("res://Scenes/Asteroid.tscn");
private int asteroidSize = 16;
private int asteroidSizeMultiplier = 20;
private int currentHealth;
private CollisionShape3D collisionShape;
private MeshInstance3D meshInstance;
public override void _Ready()
{
    currentHealth = maxHealth;
    collisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
    meshInstance = GetNode<MeshInstance3D>("MeshInstance3D");
    Node mainScene = GetTree().Root.GetChild(0);

    if(collisionShape != null){
        collisionShape.Scale = new Vector3(asteroidSize * asteroidSizeMultiplier, asteroidSize * asteroidSizeMultiplier, asteroidSize * asteroidSizeMultiplier);
    }

    if(meshInstance != null){
        meshInstance.Scale = new Vector3(asteroidSize * asteroidSizeMultiplier, asteroidSize* asteroidSizeMultiplier, asteroidSize* asteroidSizeMultiplier);
    }
}

public void takeDamage(int damage, int splitFactor, Vector3 hitLocation, Basis hitRotation){
    currentHealth -= damage;
    if(currentHealth <= 0){
        splitAsteroid(splitFactor, hitLocation, hitRotation);
    }
}


void splitAsteroid(int splitFactor, Vector3 hitLocation, Basis hitRotation){
    mainScene = GetTree().Root.GetChild(0);

    RandomNumberGenerator rng = new RandomNumberGenerator();
    int splitAsteroidCount = rng.RandiRange(minAsteroidsOnSplit, maxAsteroidsOnSplit);
    int firstSizeCount = rng.RandiRange(1, splitAsteroidCount/2);
    int secondSizeCount = rng.RandiRange(1, splitAsteroidCount-firstSizeCount);
    int thirdSizeCount = splitAsteroidCount - (firstSizeCount + secondSizeCount);

    if(asteroidSize > splitFactor){
        for(int i =1; i < 3; i++){
            SpawnAsteroid(splitFactor, hitLocation, hitRotation);
        }
    }



        GD.Print("Total of asteroids spawned: " + splitAsteroidCount + " Big ones: " + firstSizeCount + " Medium ones: " + secondSizeCount + " Small ones: " + thirdSizeCount);
        QueueFree();
    }

    void SpawnAsteroid(int splitFactor, Vector3 hitLocation, Basis hitRotation){
            Asteroid asteroid = (Asteroid)asteroidScene.Instantiate();

            asteroid.asteroidSize = asteroidSize - splitFactor;
            mainScene.AddChild(asteroid);

            //Vector3 direction = (i % 2 == 0 ? hitRotation.X : -hitRotation.X).Normalized();
            Vector3 direction = hitRotation.Y.Normalized();


            float spreadDistance = asteroid.asteroidSize * asteroidSizeMultiplier / 2f;
            //float spreadDistance = 0;

            asteroid.GlobalPosition = hitLocation + direction * spreadDistance;

            GD.Print(asteroid.GlobalPosition);
    }
}
