using Godot;
using System;

public class Screen : Spatial
{
    [Export]
    private Material stage1;
    [Export]
    private Material stage2;
    private MeshInstance meshInstance;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        meshInstance = GetNode<MeshInstance>("screen");

        meshInstance.SetSurfaceMaterial(1, stage1);
    }

    public void change()
    {
        meshInstance.SetSurfaceMaterial(1, stage2);
    }
}
