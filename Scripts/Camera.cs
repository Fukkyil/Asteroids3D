using Godot;


public partial class Camera : Camera3D
{
    [Export]
    public bool invert_y;
    [Export]
    public bool invert_x;
    [Export]
    public float mouse_sensitivity = 0.05f;
    [Export]
    public float max_fov = 120;
    [Export]
    public float default_fov = 90;
    [Export]
    public float min_fov = 80;
    [Export]
    public float fov_multiplier = 1f;
    [Export]
    public float fov_lerp_speed = 0.1f;
    [Export]
    public float camera_lerp_speed = 100f;
    [Export]
    public float maximum_mouse_rotation = 90;
    [Export]
    public Vector3 offset = new Vector3(0, 30, 85);


    private ShipController shipController;
    private int dirY;
    private int dirX;

    private Vector2 viewport;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        shipController = GetNode<ShipController>("../");
        viewport = GetWindow().Size;
        
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
            float deltaX = Mathf.Clamp(mouseEvent.Relative.X, -maximum_mouse_rotation, maximum_mouse_rotation);
            float deltaY = Mathf.Clamp(mouseEvent.Relative.Y, -maximum_mouse_rotation, maximum_mouse_rotation);

            if (mouseEvent.Relative.X != 0)
            {
                shipController.yaw_input = dirX * deltaX * mouse_sensitivity;
                //yaw
            }

            if (mouseEvent.Relative.Y != 0)
            {
                shipController.pitch_input = dirY * deltaY * mouse_sensitivity;
                //Pitch
            }

        }

    }

    public override void _Process(double delta)
    {
        if(Input.MouseMode is Input.MouseModeEnum.Captured && Input.IsActionPressed("ui_back")){
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else if(Input.MouseMode is Input.MouseModeEnum.Visible && Input.IsActionPressed("ui_back")){
            Input.MouseMode = Input.MouseModeEnum.Captured;
        } 

        Input.WarpMouse(new Vector2(viewport.X/2, viewport.Y/2));


        //FOV Adjustments
        if(shipController.currentThrustState is ShipController.thrustState.Forward){
            Fov = Mathf.Lerp(Fov, max_fov * fov_multiplier, (shipController.velocity.Length() * fov_lerp_speed) * (float)delta);
        }
        else if(shipController.currentThrustState is ShipController.thrustState.Backwards){
            Fov = Mathf.Lerp(Fov, min_fov * fov_multiplier, (shipController.velocity.Length() * fov_lerp_speed) * (float)delta);
        }
        else{
            Fov = Mathf.Lerp(Fov, default_fov * fov_multiplier, (shipController.velocity.Length() * fov_lerp_speed) * (float)delta);
        }
        GD.Print("Current FOV:" + Fov);


        Vector3 rotatedOffset = shipController.GlobalTransform.Basis * offset;
        GlobalPosition = shipController.GlobalPosition + rotatedOffset;

        //GlobalRotation = shipController.GlobalRotation;

        Quaternion shipRotation = shipController.GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion cameraRotation = GlobalTransform.Basis.GetRotationQuaternion();

        //Spherical Linear Interpolation for the camera
        Quaternion slerpRotation = cameraRotation.Slerp(shipRotation, camera_lerp_speed * (float)delta);

        Basis slerpBasis = new Basis(slerpRotation);
        GlobalTransform = new Transform3D(slerpBasis, GlobalPosition);
    }
}
