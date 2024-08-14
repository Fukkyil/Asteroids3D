using Godot;
using System;

public partial class ShipUI : Control
{
	public static ShipUI Instance { get; private set; }
	private Label shipRotationLabel;
	private Label shipPositionLabel;
	private Label shipMomentumLabel;
	private TextureRect shipCursor;
	private Camera cameraNode;
	private Spaceship spaceshipNode;

	public override void _Ready()
	{
		Instance = this;
		shipRotationLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipRotation");
		shipPositionLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipPosition");
		shipMomentumLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipMomentum");
		shipCursor = GetNode<TextureRect>("PanelContainer2/TextureRect");
	}

    public override void _Process(double delta)
    {
        updateShipMouse();
    }

    public void UpdateUI(Vector3 Rotation, Vector3 Position, Vector3 Momentum){
		shipRotationLabel.Text = Rotation.ToString("F3");
		shipPositionLabel.Text = Position.ToString("F2");
		shipMomentumLabel.Text = Momentum.ToString("F3");
	}

	public void SetCameraNode(Camera node){
		cameraNode = node;
	}
	public void SetSpaceshipNode(Spaceship node){
		spaceshipNode = node;
	}

	protected void updateShipMouse(){
		Vector3 shipForward = -spaceshipNode.GlobalTransform.Basis.Z;

		Vector2 screenCenter = new Vector2(ShipUI.Instance.Size.X / 2, 120);

		Vector2 screenPosition = cameraNode.UnprojectPosition(spaceshipNode.GlobalTransform.Origin + shipForward);

		screenPosition -= shipCursor.Size / 2;

		Vector3 cameraToShip = spaceshipNode.GlobalTransform.Origin - cameraNode.GlobalTransform.Origin;
		bool isVisible = cameraToShip.Dot(-cameraNode.GlobalTransform.Basis.Z) > 0;

		if (isVisible){
			shipCursor.Position = screenPosition + screenCenter;
			shipCursor.Visible = true;
			GD.Print("Difference: ", cameraNode.GlobalPosition - spaceshipNode.GlobalPosition);
		}
		else{
			shipCursor.Visible = false;
			GD.Print("ShipCursor is behind camera!!");
		}
		
	}
}
