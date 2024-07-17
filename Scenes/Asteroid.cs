using Godot;
using System;

public partial class Asteroid : RigidBody3D
{  
    [Export]
    public int maxHealth = 100;
    private int currentHealth;
    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    void _on_body_entered(Node Body){


        if(Body is Projectile){
            Projectile projectile = (Projectile)Body;


            if(projectile.canHitAsteroids){
                takeDamage(projectile.asteroidDamage);
            }

        }

    }

    private void takeDamage(int damage){
        currentHealth -= damage;
        GD.Print("Ouch, I(asteroid) got hit and took" + damage + "! I have" + currentHealth + "health");
        if(currentHealth <= 0){
            QueueFree();
        }
    }












}
