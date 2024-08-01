using Godot;
using System;

public partial class AutoloadGeneral : Node
{   
    [Export]
    public bool isUIOpen = false;

    public static AutoloadGeneral Instance { get; private set;}

    public override void _Ready()
    {
        Instance = this;
    }
    public override void _Process(double delta)
    {
        if(Input.MouseMode is Input.MouseModeEnum.Captured && (Input.IsActionJustPressed("ui_back") || isUIOpen)){
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else if(Input.MouseMode is Input.MouseModeEnum.Visible && Input.IsActionJustPressed("ui_back")){
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }
}
