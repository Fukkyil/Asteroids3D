using Godot;


public partial class Camera : Camera3D
{
    [Export]
    public float cameraMaxFov = 120;
    [Export]
    public float cameraMinFov = 80;
    [Export]
    public float cameraFovMultiplier = 1f;
    [Export]
    public float cameraFovLerpSpeed = 0.1f;
    [Export]
    public float cameraPosLerpSpeed = 80f;

    private Spaceship shipController;
    public override void _Ready()
    {
        setupCamera();
    }
    public override void _Process(double delta)
    {
        followShip(delta);
    }


    protected void setupCamera(){
        Input.MouseMode = Input.MouseModeEnum.Captured;
        shipController = GetNode<Spaceship>("../ViperClass");
        
        if(shipController != null){
            GD.Print("Shipcontroller found!");
            GD.Print(shipController);
        }
        else{
            GD.Print("Shipcontroller not found!");
        }
    }

    protected void adjustFOV(double delta){
        Fov = Mathf.Lerp(Fov, cameraMaxFov * cameraFovMultiplier, shipController.GetShipVelocity().Length() * cameraFovLerpSpeed * (float)delta);
    }

    protected void followShip(double delta){
        Vector3 rotatedOffset = shipController.GlobalTransform.Basis * shipController.GetCameraOffset();
        GlobalPosition = shipController.GlobalPosition + rotatedOffset;

        Quaternion shipRotation = shipController.GlobalTransform.Basis.GetRotationQuaternion();
        Quaternion cameraRotation = GlobalTransform.Basis.GetRotationQuaternion();

        //Spherical Linear Interpolation for the camera
        Quaternion slerpRotation = cameraRotation.Slerp(shipRotation, cameraPosLerpSpeed * (float)delta);
        //Quaternion slerpRotation = shipRotation;

        Basis slerpBasis = new Basis(slerpRotation);
        GlobalTransform = new Transform3D(slerpBasis, GlobalPosition);

    }
}
