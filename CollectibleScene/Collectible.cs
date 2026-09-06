using Godot;
using System;

public partial class Collectible : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEnter;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotate(new Vector3(0, 1, 0), (float)delta * 1.5f);
		Rotate(new Vector3(0, 0, 1), (float)delta);
	}

	public void OnAreaEnter(Area3D area)
	{
		GD.Print("Collected Collectible!");
	}
}
