using Godot;

public partial class ShipController : RigidBody3D
{
	[Export]
	public float max_speed = 50;
	[Export]
	public float natural_speed = 25;
	[Export]
	public float brake_speed = 5;
	[Export]
	public float acceleration = 0.6f;
	[Export]
	public float pitch_speed = 1.5f;
	[Export]
	public float roll_speed = 1.9f;
	[Export]
	public float yaw_speed = 1.25f;
	public float forward_speed = 30;
    public bool isthrustringforward;
    public bool isthrustingbackward;
 

	private float pitch_input = 0;
	private float yaw_input = 0;
	private float roll_input = 0;
	Vector3 velocity;

	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("throttle_up")){
			forward_speed = (float)Mathf.Lerp(forward_speed, max_speed, acceleration * delta);
            isthrustringforward = true;
            isthrustingbackward = false;
		}
		else if(Input.IsActionPressed("throttle_down")){
			forward_speed = (float)Mathf.Lerp(forward_speed, brake_speed, acceleration * delta);
            isthrustingbackward = true;
            isthrustringforward = false;
		}
		else{
			forward_speed = (float)Mathf.Lerp(forward_speed, natural_speed, acceleration * delta);
            isthrustingbackward = false;
            isthrustringforward = false;
		}


		pitch_input = Input.GetAxis("pitch_down", "pitch_up");
		yaw_input = Input.GetAxis("yaw_right", "yaw_left");
		roll_input = Input.GetAxis("roll_right", "roll_left");


		Quaternion pitch = new Quaternion(Vector3.Right, pitch_input * pitch_speed * (float)delta);
		Quaternion yaw = new Quaternion(Vector3.Up, yaw_input * yaw_speed * (float)delta);
		Quaternion roll = new Quaternion(Vector3.Forward, roll_input * roll_speed * (float)delta);

		Transform3D currentTransform = GlobalTransform;
		Quaternion currentRotation = new Quaternion(currentTransform.Basis);
	   
	   currentRotation = currentRotation * roll * pitch * yaw;
	   currentRotation.Normalized();

	   currentTransform.Basis = new Basis(currentRotation);
	   GlobalTransform = currentTransform;

	}

	public override void _PhysicsProcess(double delta)
	{
	   Vector3 forward = -GlobalTransform.Basis.Z;
	   velocity = forward * forward_speed;
	   
	   MoveAndCollide(velocity * (float)delta);
	   

	   //GD.Print("Position: " + GlobalPosition + " | speed: " + forward_speed);
	   //GD.Print("Rotation: " + GlobalRotation);
	   //GD.Print("Roll Input: " + roll_input + " Pitch Input: " + pitch_input + " Yaw Input: " + yaw_input);
	   
	}

}
