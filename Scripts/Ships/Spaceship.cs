using Godot;
using System;

public partial class Spaceship : RigidBody3D
{
    //Acceleration variables
    protected float shipMaxSpeed;
    protected float shipAcceleration;
    protected float shipForwardSpeed;
    protected Vector3 shipVelocity;

    //Rotation variables
    protected float shipPitchSpeed;
    protected float shipYawSpeed;
    protected float shipRollSpeed;

    //Rotation Quaternions
    protected Quaternion shipPitchQuat;
    protected Quaternion shipYawQuat;
    protected Quaternion shipRollQuat;

    //Input variables
    protected float shipPitchInput;
    protected float shipYawInput;
    protected float shipRollInput;
    protected float mouseDeltaY;
    protected float mouseDeltaX;
    protected float mouseMaxRotation = 200;
    protected float mouseSensitivity = 0.05f;

    //Inventory variables
    protected int shipInvSize;

    //Camera variables
    protected Vector3 shipCameraOffset;

    //Player variables
    protected Node playerNode;

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
    public override void _UnhandledInput(InputEvent @event){
        if(@event is InputEventMouseMotion mouseEvent){
            //mouseDeltaX = Mathf.Clamp(mouseEvent.Relative.X, -mouseMaxRotation, mouseMaxRotation);
                mouseDeltaX = mouseEvent.Relative.X;
            //mouseDeltaY = Mathf.Clamp(mouseEvent.Relative.Y, -mouseMaxRotation, mouseMaxRotation);
                mouseDeltaY = mouseEvent.Relative.Y;

            if(mouseEvent.Relative.Y != 0){
                shipPitchInput = -(mouseDeltaY * mouseSensitivity);
            }

            if(mouseEvent.Relative.X != 0){
                shipYawInput = -(mouseDeltaX * mouseSensitivity);
            }
        }
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

        //Rotation input mainline
        shipRollInput = Input.GetAxis("roll_right", "roll_left");

        //Mouse input mainline
        /*if (mouseDeltaX != 0){
            shipYawInput = -(mouseDeltaX * mouseSensitivity);
        }
        if (mouseDeltaY != 0)
        {
            shipPitchInput = -(mouseDeltaY * mouseSensitivity);
        }*/

    }

    protected void manageRotation(double delta){
        //Setting rotations based on the inputs
		shipPitchQuat = new Quaternion(Vector3.Right, shipPitchInput * shipPitchSpeed * (float)delta);
		shipYawQuat = new Quaternion(Vector3.Up, shipYawInput * shipYawSpeed * (float)delta);
		shipRollQuat = new Quaternion(Vector3.Forward, shipRollInput * shipRollSpeed * (float)delta);

        Transform3D currentTransform = GlobalTransform;
	    Quaternion currentRotation = new Quaternion(currentTransform.Basis);
	   
	    currentRotation = currentRotation * shipRollQuat * shipPitchQuat * shipYawQuat;
	    currentRotation.Normalized();

	    currentTransform.Basis = new Basis(currentRotation);
	    GlobalTransform = currentTransform;

		shipYawInput = 0;
		shipPitchInput = 0;
		shipRollInput = 0;
    }

    protected void manageMovement(double delta){
        Vector3 forward = -GlobalTransform.Basis.Z;
        shipVelocity += forward * shipForwardSpeed * (float)delta;

        if(shipVelocity.Length() > shipMaxSpeed){
            shipVelocity = shipVelocity.Normalized() * shipMaxSpeed;
        }
        
        MoveAndCollide(shipVelocity);
    }

}
