using Godot;
using System;

public class MonsterDay2Move : Spatial
{
    bool moveStarted = false;

    SceneTreeTimer timer;

    float speed = 1.3f;

    private AudioStreamPlayer3D sfx;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer3D>("SFX");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (moveStarted)
        {
            Translate(new Vector3(0, 0, speed * delta));
        }
    }

    public void StartMove(Node body)
    {
        if (body is Player && !moveStarted)
        {
            moveStarted = true;
            timer = GetTree().CreateTimer(15.0f);
            timer.Connect("timeout", this, nameof(FinishEvent));
            sfx.Play();
        }
    }

    public void FinishEvent()
    {
        QueueFree();
    }
}
