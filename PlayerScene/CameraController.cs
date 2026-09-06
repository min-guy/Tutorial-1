using Godot;
using System;

public partial class CameraController : Camera3D
{
	// Class Variables/Refereces
	public CharacterBody3D player; // need to figure out how to reference the parent of this node
	private Vector3 offset;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		offset = this.Position - player.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
