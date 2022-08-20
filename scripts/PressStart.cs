using Godot;
using System;

public class PressStart : Label
{
    private PackedScene next;

    public override void _Ready()
    {
        next = ResourceLoader.Load<PackedScene>("res://levels/day_1.tscn");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
        {
            GetTree().ChangeSceneTo(next);
        }
    }
}
    
