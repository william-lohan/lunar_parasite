using Godot;
using System;

public class MonsterEating : AnimationPlayer
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Play("monsterEating");
    }
}
