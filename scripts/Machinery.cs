using Godot;
using System;

public class Machinery : StaticBody
{
    [Export]
    private bool needToFix;

    private Label prompt;

    private bool canFix = false;

    private string promptText = "press [E] to fix";

    private AudioStreamPlayer3D sfx;

    private Particles sparks;

    [Signal]
    delegate void Fixed();

    private void fix()
    {
        needToFix = false;
        canFix = false;
        GD.Print("play");
        sfx.Play();
        sparks.Emitting = false;
        if (prompt.Text == promptText)
        {
            prompt.Text = "";
        }
        EmitSignal(nameof(Fixed));
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
        sfx = GetNode<AudioStreamPlayer3D>("SFX");
        sparks = GetNode<Particles>("Particles");
        if (needToFix) {
            sparks.Emitting = true;
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsActionPressed("USE") && needToFix && canFix)
            {
                fix();
            }
        }
    }


    public void OnBodyEnter(Node body)
    {
        if (body is Player player && needToFix)
        {
            canFix = true;
            prompt.Text = promptText;
        }
    }

    public void OnBodyExit(Node body)
    {
        if (body is Player player)
        {
            canFix = false;
            if (prompt.Text == promptText)
            {
                prompt.Text = "";
            }
        }
    }
}
