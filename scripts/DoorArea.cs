using Godot;
using System;

public class DoorArea : Area
{

    private Label prompt;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
        Connect("body_entered", this, nameof(OnBodyEntered));
        Connect("body_exited", this, nameof(OnBodyExited));
    }

    public void OnBodyEntered(Node body)
    {
        if (body is Player player)
        {
            if (player.hasKey)
            {
                prompt.Text = "press [E]";
            }
            else
            {
                prompt.Text = "need key";
            }
        }
    }

    public void OnBodyExited(Node body)
    {
        if (body is Player player && prompt.Text == "press [E]")
        {
            prompt.Text = "";
        }
    }

}
