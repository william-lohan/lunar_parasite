using Godot;
using System;

public class Computer : Spatial
{
    [Export]
    bool interactive = false;
    [Export(PropertyHint.MultilineText)]
    string message = "";
    private Label prompt;
    private bool canUse = false;
    private string promptText = "press [E] to use";
    private Label displayText;

    private AudioStreamPlayer3D sfx;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
        displayText = GetNode<Label>("Label");
        sfx = GetNode<AudioStreamPlayer3D>("SFX");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsActionPressed("USE") && interactive && canUse)
            {
                displayText.Text = message;
                sfx.Play();
            }
        }
    }

    public void OnBodyEnter(Node body)
    {
        if (body is Player player && interactive)
        {
            canUse = true;
            prompt.Text = promptText;
        }
    }

    public void OnBodyExit(Node body)
    {
        if (body is Player player)
        {
            canUse = false;
            displayText.Text = "";
            if (prompt.Text == promptText)
            {
                prompt.Text = "";
            }
        }
    }
}
