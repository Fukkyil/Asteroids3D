using Godot;

public partial class ShipController : RigidBody3D
{
	[Export]
	public float max_speed = 7;
	[Export]
	public float acceleration = 50f;
	[Export]
	public float pitch_speed = 1f;
	[Export]
	public float roll_speed = 1f;
	[Export]
	public float yaw_speed = 1f;
	[Export]
	public float shotCooldown = 5f;

	private Timer shootTimer;

	private PackedScene bulletScene = (PackedScene)ResourceLoader.Load("res://Scenes/Projectiles.tscn");
	private float forward_speed = 0;

	public enum thrustState{
		Forward,
		Backwards,
		Idle,
	};
	public thrustState currentThrustState = thrustState.Idle;
	private Quaternion pitch = new Quaternion();
	private Quaternion yaw = new Quaternion();
	private Quaternion roll = new Quaternion();
 

	public float pitch_input = 0;
	public float yaw_input = 0;
	public float roll_input = 0;
	public Vector3 velocity;


	public override void _Process(double delta)
	{

		if(Input.IsActionPressed("throttle_up")){
			forward_speed = acceleration * (float)delta;
			currentThrustState = thrustState.Forward;
		}
		else if(Input.IsActionPressed("throttle_down")){
			forward_speed = -acceleration * (float)delta;
			currentThrustState = thrustState.Backwards;
		}
		else{
			forward_speed = 0;
			currentThrustState = thrustState.Idle;
		}

		if(Input.IsActionPressed("mouse_left")){
			Shoot();
		}



		setRotation(delta);
	}


    public override void _PhysicsProcess(double delta)
	{
	   Vector3 forward = -GlobalTransform.Basis.Z;
	   velocity = velocity + forward * forward_speed * (float)delta;

	   if(velocity.Length() > max_speed){
		velocity = velocity.Normalized() * max_speed;
	   }
	   //GD.Print("Current speed:" + velocity.Length());


	   MoveAndCollide(velocity);
	   
	}



	public void setRotation(double delta){
		roll_input = Input.GetAxis("roll_right", "roll_left");
		pitch = new Quaternion(Vector3.Right, pitch_input * pitch_speed * (float)delta);
		yaw = new Quaternion(Vector3.Up, yaw_input * yaw_speed * (float)delta);
		roll = new Quaternion(Vector3.Forward, roll_input * roll_speed * (float)delta);
		
		
		Transform3D currentTransform = GlobalTransform;
		Quaternion currentRotation = new Quaternion(currentTransform.Basis);
	   
	   currentRotation = currentRotation * roll * pitch * yaw;
	   currentRotation.Normalized();

	   currentTransform.Basis = new Basis(currentRotation);
	   GlobalTransform = currentTransform;

		yaw_input = 0;
		pitch_input = 0;
		roll_input = 0;
	}
	public async void Shoot(){
		var bullet = (projectile)bulletScene.Instantiate();
		Owner.AddChild(bullet);
		bullet.GlobalPosition = GetNode<Node3D>("MainGun").GlobalPosition;
		bullet.rotation = -GlobalTransform.Basis.Z;

		await ToSignal(GetTree().CreateTimer(shotCooldown), SceneTreeTimer.SignalName.Timeout);
	}

}
