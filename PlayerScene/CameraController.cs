using Godot;
using System;

public partial class CameraController : Camera3D
{
	// Class Variables/Refereces
	public CharacterBody3D player;
	private Vector3 offset;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody3D>(new NodePath(".."));
		
		offset = this.Position - player.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
