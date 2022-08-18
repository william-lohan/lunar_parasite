using Godot;
using System;

public class Key : Area
{

    private Spatial keyObject;
    private Label prompt;
    private bool collected = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        keyObject = GetNode<Spatial>("key");
        prompt = GetTree().Root.GetNode<Label>("Root/HUD/Prompt");
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(Node body)
    {
        if (!collected && body is Player player)
        {
            player.hasKey = true;
            keyObject.QueueFree();
            prompt.Text = "acquired key";
            collected = true;
        }
    }
}
