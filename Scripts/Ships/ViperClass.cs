using Godot;
using System;

public partial class ViperClass : Spaceship
{
    public ViperClass(){
        shipMaxSpeed = 7f;
        shipAcceleration = 50f;

        shipRotationSpeed = 0.5f;

        shipCameraOffset = new Vector3(0, 13, 20);

        PrimaryWeaponCount = 1;
        SecondaryWeaponCount = 2;
        TertiaryWeaponCount = 0;
    }
}
