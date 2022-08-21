using Godot;
using System;

public class MonsterDay4 : Spatial
{
    private AudioStreamPlayer3D sfx;

    private bool isEvent = false;

    private float target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer3D>("SFX");
        target = Translation.z - 2.0f;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!sfx.Playing && isEvent)
        {
            sfx.Play();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (isEvent && Translation.z > target)
        {
            Translate(new Vector3(0, 0, delta * 2.0f));
        }
    }

    public void Day4Event(Node body)
    {
        if (body is Player player)
        {
            isEvent = true;
        }
    }
}
