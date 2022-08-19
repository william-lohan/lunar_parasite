using Godot;
using System;

public class Screen : Spatial
{
    private Material fixMat;
    private MeshInstance meshInstance;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        fixMat = ResourceLoader.Load("res://assets/screen/screen_fix.material") as Material;
        meshInstance = GetNode<MeshInstance>("screen");

        meshInstance.SetSurfaceMaterial(1, fixMat);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
