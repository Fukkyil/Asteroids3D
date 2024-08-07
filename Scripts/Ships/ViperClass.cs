using Godot;
using System;

public partial class ViperClass : Spaceship
{
    public ViperClass(){
        shipMaxSpeed = 7f;
        shipAcceleration = 50f;
        shipPitchSpeed = 1f;
        shipYawSpeed = 1f;
        shipRollSpeed = 1f;

        shipCameraOffset = new Vector3(0, 11, 20);
    }
}
