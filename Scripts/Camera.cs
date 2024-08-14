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

    //Mouse input
    private float cameraPitchInput;
    private float cameraYawInput;
    private float cameraRollInput;
    private float mouseSensitivity = 0.05f;

    private Quaternion cameraPitchQuat;
    private Quaternion cameraYawQuat;
    private Quaternion cameraRollQuat;

    private Spaceship shipController;
    public override void _Ready()
    {
        setupCamera();
    }
    public override void _Process(double delta)
    {
        //followShip(delta);
        manageRotation(delta);
    }

    public override void _UnhandledInput(InputEvent @event){
        if(@event is InputEventMouseMotion mouseEvent){
                float mouseDeltaX = mouseEvent.Relative.X;
                float mouseDeltaY = mouseEvent.Relative.Y;

            if(mouseEvent.Relative.Y != 0){
                cameraPitchInput = -(mouseDeltaY * mouseSensitivity);
            }

            if(mouseEvent.Relative.X != 0){
                cameraYawInput = -(mouseDeltaX * mouseSensitivity);
            }
        }
    }


    protected void setupCamera(){
        Input.MouseMode = Input.MouseModeEnum.Captured;
        shipController = GetNode<Spaceship>("../ViperClass");
        
        if(shipController != null){
            GD.Print("Shipcontroller found!");
            GD.Print(shipController);
            shipController.SetCameraNode(this);
            ShipUI.Instance.SetCameraNode(this);
        }
        else{
            GD.Print("Shipcontroller not found!");
        }
    }

    protected void adjustFOV(double delta){
        Fov = Mathf.Lerp(Fov, cameraMaxFov * cameraFovMultiplier, shipController.GetShipVelocity().Length() * cameraFovLerpSpeed * (float)delta);
    }

    public void followShip(double delta){
        Vector3 rotatedOffset = shipController.GlobalTransform.Basis * shipController.GetCameraOffset();
        GlobalPosition = shipController.GlobalPosition + rotatedOffset;
    }

    protected void manageRotation(double delta){
        cameraRollInput = Input.GetAxis("roll_right", "roll_left");
        //Setting rotations based on the inputs

		cameraPitchQuat = new Quaternion(Vector3.Right, cameraPitchInput * (float)delta);
		cameraYawQuat = new Quaternion(Vector3.Up, cameraYawInput * (float)delta);
		cameraRollQuat = new Quaternion(Vector3.Forward, cameraRollInput * (float)delta);

        Transform3D currentTransform = GlobalTransform;
	    Quaternion currentRotation = new Quaternion(currentTransform.Basis);
	   
        //GD.Print("Pitch: " + cameraPitchQuat + " | Yaw: " + cameraYawQuat + "| Roll: " + cameraRollQuat);
	    currentRotation = currentRotation * cameraRollQuat * cameraPitchQuat * cameraYawQuat;
	    currentRotation.Normalized();

	    currentTransform.Basis = new Basis(currentRotation);
	    GlobalTransform = currentTransform;

		cameraYawInput = 0;
		cameraPitchInput = 0;
		cameraRollInput = 0;
    }
}
