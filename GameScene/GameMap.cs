using Godot;
using System;

public partial class GameMap : Node3D
{
	private DirectionalLight3D globalLight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globalLight = GetNode<DirectionalLight3D>(new NodePath("DirectionalLight3D"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		globalLight.Rotate(new Vector3(0, 1, 0), (float)delta * 0.065f);
		globalLight.Rotate(new Vector3(1, 0, 0), (float)delta * 0.085f);
	}
}
