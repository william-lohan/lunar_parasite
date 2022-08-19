using Godot;
using System;

public class Door : Spatial
{
    private Label prompt;

    private AnimationPlayer animationPlayer;

    private Particles sparks;

    private bool canOpen = false;

    private void OpenDoor()
    {
        animationPlayer.Play("ArmatureAction");
        if (prompt.Text == "press [E]")
        {
            prompt.Text = "";
        }
        sparks.Emitting = true;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        sparks = GetNode<Particles>("Particles");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsActionPressed("USE") && canOpen)
            {
                OpenDoor();
            }
        }
    }

    public void OnBodyEnter(Node body)
    {
        if (body is Player player)
        {
            if (player.hasKey)
            {
                canOpen = true;
                prompt.Text = "press [E]";
            }
            else
            {
                prompt.Text = "need key";
            }
        }
    }

    public void OnBodyExit(Node body)
    {
        if (body is Player player)
        {
            canOpen = false;
            if (prompt.Text == "press [E]")
            {
                prompt.Text = "";
            }
        }
    }
}
