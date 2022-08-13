using Godot;
using System;

public class PressStart : Label
{
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
        {
            // TODO
            GetTree().ChangeScene("res://levels/moon_base.tscn");
        }
    }
}
    
