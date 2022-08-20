using Godot;
using System;

public class Bed : StaticBody
{
    [Export]
    private PackedScene next;

    private Label prompt;

    private bool canSleep = false;

    private string promptText = "press [E] to sleep";


    private void goToSleep()
    {
        GetTree().ChangeSceneTo(next);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsActionPressed("USE") && canSleep)
            {
                goToSleep();
            }
        }
    }


    public void OnBodyEnter(Node body)
    {
        if (body is Player player)
        {
            canSleep = true;
            prompt.Text = promptText;
        }
    }

    public void OnBodyExit(Node body)
    {
        if (body is Player player)
        {
            canSleep = false;
            if (prompt.Text == promptText)
            {
                prompt.Text = "";
            }
        }
    }


}
