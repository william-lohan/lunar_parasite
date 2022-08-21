using Godot;
using System;

public class Door : Spatial
{
    private Label prompt;

    private AnimationPlayer animationPlayer;

    private Particles sparks;

    private AudioStreamPlayer3D sfx;

    private bool canOpen = false;

    private string promptText = "press [E] to open";

    private bool isDay4Event = false;

    private void OpenDoor()
    {
        animationPlayer.Play("ArmatureAction");
        sfx.Play();
        if (prompt.Text == promptText)
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
        sfx = GetNode<AudioStreamPlayer3D>("SFX");
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
                prompt.Text = promptText;
            }
            else if (isDay4Event)
            {
                GetTree().ChangeScene("res://levels/end.tscn");
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
            if (prompt.Text == promptText)
            {
                prompt.Text = "";
            }
        }
    }

    public void Day4Event(Node body)
    {
        if (body is Player player)
        {
            isDay4Event = true;
            OpenDoor();
        }
    }

}
