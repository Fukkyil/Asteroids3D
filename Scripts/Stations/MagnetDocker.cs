using Godot;
using Structures.Stations;
using System;

public partial class MagnetDocker : Node3D
{
    [Export]
    public float lockAreaSize = 2;
    [Export]
    public float pullAreaSize = 15;
    [Export]
    public float scaleMultiplier = 3;
    [Export]
    public Vector3 magnetOffsetPosition = new Vector3(0,0,0);
    [Export]	
    public Vector3 magnetOffsetRotation = new Vector3(0,0,0);

    //General force multiplier
    [Export]
    public float magnetForceMultiplier = 3f;
    //Push force multiplier
    [Export]
    public float magnetPushMultiplier = 5f;
    //Pull force multiplier
    [Export]
    public float magnetPullMultiplier = 1;

    public Quaternion shipLockedYaw = new Quaternion(Vector3.Up, MathF.PI/2);
    public Quaternion shipLockedPitch = new Quaternion(Vector3.Right, 0);
    public Quaternion shipLockedRoll = new Quaternion(Vector3.Forward, MathF.PI/2);
    
    private bool isPlayerLocked = false;
    private bool isPlayerInField = false;
    private bool isMagnetPush = false;
    private ShipController player;


    private CollisionShape3D lockAreaCollisionNode;
    private Node3D pullAreaCollisionNode;
    private Node3D magnetMeshNode;
    private Node3D lockAreaVisual;
    private Node3D pullAreaVisual;

    private SpaceStation parentStation;

    public override void _Ready(){
        parentStation = GetParent<SpaceStation>();
        StartNodes();
        SetScales();
    }

    public override void _Process(double delta)
    {
        if(isPlayerInField && !isPlayerLocked){
            float playerDistance = player.GlobalTransform.Origin.DistanceTo(lockAreaCollisionNode.GlobalTransform.Origin);
            float adjustedDistance = 1f - playerDistance/(pullAreaSize*scaleMultiplier*2);
            float adjustedForce = magnetForceMultiplier * adjustedDistance;
            Vector3 lockAreaDirection = (lockAreaCollisionNode.GlobalTransform.Origin - player.GlobalTransform.Origin).Normalized();

            //if pushmode = true then it'll be positive, if false then it'll be negative to make it so the magnet can pull and push.
            float magnetForceModifier = isMagnetPush ? -1*magnetPushMultiplier : 1*magnetPullMultiplier;

            player.velocity += lockAreaDirection * magnetForceModifier * adjustedForce * (float)delta;
        }
    }

    private void _on_lock_area_body_exited(Node Body){
        if(Body is ShipController){
            isPlayerLocked = false;
            parentStation.UndockShip();
        }
    }

    private void _on_lock_area_body_entered(Node Body){
        if(Body is ShipController){
            player.Position = GlobalTransform.Origin + (-Basis.Z.Normalized() * 2.2f * scaleMultiplier);
            player.velocity = Vector3.Zero;

            Quaternion magnetRotation = new Quaternion(GlobalTransform.Basis);
            Quaternion finalRotation = magnetRotation * shipLockedYaw * shipLockedPitch * shipLockedRoll;

            finalRotation.Normalized();
            player.GlobalTransform = new Transform3D(new Basis(finalRotation), player.GlobalTransform.Origin);
            isPlayerLocked = true;
            isMagnetPush = true;
            parentStation.DockShip();
        }
        else{
            GD.Print(Body + " Just entered the magnet lock area");
        }
    }


    private void _on_pull_area_body_entered(Node Body){
        if(Body is ShipController){

            if(player == null){
            player = (ShipController)Body;
            }

            isPlayerInField = true;
        }

        else{
            GD.Print(Body + " Just entered the magnet pull area");
        }
    }

    private void _on_pull_area_body_exited(Node Body){
        if(Body is ShipController){
            isPlayerInField = false;
            isMagnetPush = false;
        }
    }

    private void StartNodes(){
        lockAreaCollisionNode = GetNode<CollisionShape3D>("LockArea/CollisionShape3D");
        pullAreaCollisionNode = GetNode<CollisionShape3D>("PullArea/CollisionShape3D");
        magnetMeshNode = GetNode<MeshInstance3D>("MeshInstance3D");
        lockAreaVisual = GetNode<MeshInstance3D>("LockArea/CollisionShape3D/MeshInstance3D");
        pullAreaVisual = GetNode<MeshInstance3D>("PullArea/CollisionShape3D/MeshInstance3D");

        if(lockAreaCollisionNode != null){GD.Print("Lock area found!");}
        else{GD.Print("Lock area not found");}

        if(pullAreaCollisionNode != null){GD.Print("Pull area found!");}
        else{GD.Print("Pull area not found");}

        if(magnetMeshNode != null){GD.Print("Magnet Mesh found!");}
        else{GD.Print("Magnet mesh not found");}

        if(lockAreaVisual != null){GD.Print("Lock area Mesh found!");}
        else{GD.Print("Lock area Mesh not found");}

        if(pullAreaVisual != null){GD.Print("Pull area Mesh found!");}
        else{GD.Print("Pull area Mesh not found");}

    }

    private void SetScales(){
        Vector3 lockAreaScale = new Vector3(lockAreaSize*scaleMultiplier, lockAreaSize*scaleMultiplier, lockAreaSize*scaleMultiplier);
        Vector3 pullAreaScale = new Vector3(pullAreaSize*scaleMultiplier, pullAreaSize*scaleMultiplier, pullAreaSize*scaleMultiplier);

        Vector3 lockAreaPosition = magnetMeshNode.GlobalTransform.Origin + (-magnetMeshNode.Basis.Z.Normalized() * lockAreaScale);
        Vector3 pullAreaPosition = magnetMeshNode.GlobalTransform.Origin + (-magnetMeshNode.Basis.Z.Normalized() * pullAreaScale);

        lockAreaCollisionNode.Translate(magnetMeshNode.ToLocal(lockAreaPosition));

        pullAreaCollisionNode.Translate(magnetMeshNode.ToLocal(pullAreaPosition));

        lockAreaCollisionNode.Scale = lockAreaScale;

        pullAreaCollisionNode.Scale = pullAreaScale;

        magnetMeshNode.Scale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
    }

}
