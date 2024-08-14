using Godot;

public partial class Spaceship : RigidBody3D
{
    //Weapon variables
    protected WeaponSlotManager slotManager;
    protected Node3D[] WeaponSlotOrigins;
    

    //Acceleration variables
    protected float shipMaxSpeed;
    protected float shipAcceleration;
    protected float shipForwardSpeed;
    protected Vector3 shipVelocity;

    //Rotation variables
    [Export]
    protected float shipRotationSpeed;

    //Rotation Quaternions
    protected Quaternion shipPitchQuat;
    protected Quaternion shipYawQuat;
    protected Quaternion shipRollQuat;

    //Inventory variables
    protected int shipInvSize;

    //Camera variables
    protected Vector3 shipCameraOffset;
    protected Camera shipCameraNode;

    //Player variables
    protected Node playerNode;


    public override void _Ready()
    {
        ShipUI.Instance.SetSpaceshipNode(this);
    }
    public override void _Process(double delta)
    {
        manageInputs(delta);
        manageRotation(delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        manageMovement(delta);
    }

    public Vector3 GetShipVelocity(){
        return shipVelocity;
    }

    public Vector3 GetCameraOffset(){
        return shipCameraOffset;
    }

    public void SetCameraNode(Camera camera){
        shipCameraNode = camera;
    }
    protected void manageInputs(double delta){
        //Throttle input mainline
        if(Input.IsActionPressed("throttle_up")){
            shipForwardSpeed = shipAcceleration * (float)delta;
        }
        else if(Input.IsActionPressed("throttle_down")){
            shipForwardSpeed = -shipAcceleration * (float)delta;
        }
        else{
            shipForwardSpeed = 0;
        }
    }

    protected void manageRotation(double delta){
        Transform3D currentTransform = GlobalTransform;
        Quaternion currentRotation = new Quaternion(GlobalTransform.Basis);
        Quaternion cameraRotation = new Quaternion(shipCameraNode.GlobalTransform.Basis);

        currentRotation = currentRotation.Slerp(cameraRotation, shipRotationSpeed * (float)delta);
        //1 = instant
        currentRotation.Normalized();

        currentTransform.Basis = new Basis(currentRotation);
        GlobalTransform = currentTransform;
    }

    protected void manageMovement(double delta){
        Vector3 forward = -GlobalTransform.Basis.Z;
        shipVelocity += forward * shipForwardSpeed * (float)delta;

        if(shipVelocity.Length() > shipMaxSpeed){
            shipVelocity = shipVelocity.Normalized() * shipMaxSpeed;
        }
        
        MoveAndCollide(shipVelocity);
        shipCameraNode.followShip(delta);
    }
}