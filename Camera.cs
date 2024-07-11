using Godot;
using System;
using System.Net.Http.Headers;

public partial class Camera : Node3D
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
    public float fov_larp_speed = 0.05f;

    private Vector3 offset = new Vector3(0, 5, 15);
    private Node3D innerGimbal;
    private Camera3D camera;
    private ShipController shipController;
    private int dirY;
    private int dirX;

    public override void _Ready()
    {
        innerGimbal = GetNode<Node3D>("InnerGimbal");
        shipController = GetNode<ShipController>("../Spaceship");
        camera = GetNode<Camera3D>("InnerGimbal/Camera3D");

        camera.GlobalPosition = offset + GlobalPosition;

        if(shipController != null){
            GD.Print("Shipcontroller found!");
            GD.Print(shipController);
        }
        else{
            GD.Print("Shipcontroller not found!");
        }

        if(camera != null){
            GD.Print("Camera Found!");
            GD.Print(camera);
        }
        else{
            GD.Print("Camera not found!");
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
                RotateObjectLocal(Vector3.Up, dirX * mouseEvent.Relative.X * mouse_sensitivity);
            }

            if (mouseEvent.Relative.Y != 0)
            {
                innerGimbal.RotateObjectLocal(Vector3.Right, dirY * mouseEvent.Relative.Y * mouse_sensitivity);
                GD.Print(innerGimbal.Rotation + " Ship: " + shipController.Rotation);
            }

        }
    }

    public override void _Process(double delta)
    {

        //FOV Adjustments
        if(shipController.isthrustringforward){
            camera.Fov = Mathf.Lerp(camera.Fov, max_fov * fov_multiplier, (shipController.forward_speed * fov_larp_speed) * (float)delta);
        }
        else if(shipController.isthrustingbackward){
            camera.Fov = Mathf.Lerp(camera.Fov, min_fov * fov_multiplier, (shipController.forward_speed * fov_larp_speed) * (float)delta);
        }
        else{
            camera.Fov = Mathf.Lerp(camera.Fov, default_fov * fov_multiplier, (shipController.forward_speed * fov_larp_speed) * (float)delta);
        }
        //GD.Print("Fov: " + camera.Fov);

        GlobalPosition = shipController.GlobalPosition;

        
        
    }
}
