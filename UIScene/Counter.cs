using Godot;
using System;
using System.Net.Security;

public partial class Counter : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	private Label counter;
	private int count = 0;
	
	[Export] public Player player;
	public override void _Ready()
	{
		counter = GetNode<Label>(new NodePath("Counter"));
		count = 0; // double check to make sure it's 0 at the beginning
		counter.Text = "Count: " + count; // initial count
		player.Connect(Player.SignalName.OnHitCollectible, Callable.From(IncreaseCounter));
	}

	private void IncreaseCounter()
	{
		count ++;
		counter.Text = "Count: " + count;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
