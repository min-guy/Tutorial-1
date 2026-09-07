using Godot;
using System;
using System.Net.Security;

public partial class Counter : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	private Label counter;
	const String count = "Count: ";
	
	public override void _Ready()
	{
		counter = GetNode<Label>(new NodePath("Counter"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
