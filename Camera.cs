using Godot;
using System;
using System.Net.Http.Headers;

public partial class Camera : Camera3D
{
    [Export]
    public bool invert_y;
    [Export]
    public bool invert_x;
    [Export]
    public float mouse_sensitivity = 0.005f;
    [Export]
    public float max_fov = 90;
    [Export]
    public float default_fov = 60;
    [Export]
    public float min_fov = 30;
    [Export]
    public float fov_multiplier = 1f;
    [Export]
    public float fov_lerp_speed = 0.05f;
    [Export]
    public float camera_lerp_speed = 20f;

    private Vector3 offset = new Vector3(0, 5, 15);
    private ShipController shipController;
    private int dirY;
    private int dirX;

    public override void _Ready()
    {
        shipController = GetNode<ShipController>("../Spaceship");

        
        if(shipController != null){
            GD.Print("Shipcontroller found!");
            GD.Print(shipController);
        }
        else{
            GD.Print("Shipcontroller not found!");
        }


        //Mouse Inversion setup
        //Interesting way of making an if statement I saw in a blog: var x = statement ? true : false;
        dirY = invert_y ? dirY = 1 : dirY = -1;
        dirX = invert_x ? dirX = 1 : dirX = -1;
    }

    public override void _UnhandledInput(InputEvent @event)
{
    //Camera mouse controls
        if (@event is InputEventMouseMotion mouseEvent)
        {
            if (mouseEvent.Relative.X != 0)
            {
                //RotateObjectLocal(Vector3.Up, dirX * mouseEvent.Relative.X * mouse_sensitivity);
                //Yaw
            }

            if (mouseEvent.Relative.Y != 0)
            {
                //RotateObjectLocal(Vector3.Right, dirY * mouseEvent.Relative.Y * mouse_sensitivity);
                //Pitch
            }

        }
    }

    public override void _Process(double delta)
    {

        //FOV Adjustments
        if(shipController.isthrustringforward){
            Fov = Mathf.Lerp(Fov, max_fov * fov_multiplier, (shipController.forward_speed * fov_lerp_speed) * (float)delta);
        }
        else if(shipController.isthrustingbackward){
            Fov = Mathf.Lerp(Fov, min_fov * fov_multiplier, (shipController.forward_speed * fov_lerp_speed) * (float)delta);
        }
        else{
            Fov = Mathf.Lerp(Fov, default_fov * fov_multiplier, (shipController.forward_speed * fov_lerp_speed) * (float)delta);
        }
        //GD.Print("Fov: " + Fov);

        Vector3 rotatedOffset = shipController.GlobalTransform.Basis * offset;
        GlobalPosition = shipController.GlobalPosition + rotatedOffset; 

        Quaternion shipRotation = shipController.GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion cameraRotation = GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion slerpRotation = cameraRotation.Slerp(shipRotation, camera_lerp_speed * (float)delta);

        Basis slerpBasis = new Basis(slerpRotation);
        GlobalTransform = new Transform3D(slerpBasis, GlobalPosition);
 

        //GD.Print("Camera Rotation: " + Rotation  + " Ship Rotation: " + shipController.Rotation);
        //GD.Print("Camera Position: " + GlobalPosition + "Ship Position: " + shipController.GlobalPosition);
    }
}
