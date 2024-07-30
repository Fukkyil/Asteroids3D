using Godot;
using System;

public partial class ShipUI : CanvasLayer
{
    public static ShipUI Instance { get; private set; }
    private Label shipRotationLabel;
    private Label shipPositionLabel;
    private Label shipMomentumLabel;

    public override void _Ready()
    {
        Instance = this;
        shipRotationLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipRotation");
        shipPositionLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipPosition");
        shipMomentumLabel = GetNode<Label>("PanelContainer/MarginContainer/GridContainer/shipMomentum");
    }
    
    public void UpdateUI(Vector3 Rotation, Vector3 Position, Vector3 Momentum){
        shipRotationLabel.Text = Rotation.ToString("F3");
        shipPositionLabel.Text = Position.ToString("F2");
        shipMomentumLabel.Text = Momentum.ToString("F3");
    }
}
