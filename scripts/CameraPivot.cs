using Godot;
using System;

public class CameraPivot : Spatial
{
    private float time = 0f;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        time = time + delta;

        float x = (float) Math.Sin(time * 0.0005) * 180f;
        float y = (float) Math.Sin((time * 0.0004) + 0.0002) * 180f;
        Rotation = new Vector3(x, y, 0f);
    }
}
