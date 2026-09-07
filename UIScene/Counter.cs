using Godot;
using System;
using System.Net.Security;

public partial class Counter : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	private Label counter;
	private int count = 0;

	private const int WIN_MAX = 12;
	
	public override void _Ready()
	{
		counter = GetNode<Label>(new NodePath("Counter"));
		counter.Text = "Count: " + count; // initial count
		
		GameSignals.Instance.Connect(GameSignals.SignalName.OnHitCollectible, Callable.From(IncreaseCounter));
	}

	private void IncreaseCounter()
	{
		count ++;
		counter.Text = "Count: " + count;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (count >= WIN_MAX)
		{
			GameSignals.Instance.EmitSignal(GameSignals.SignalName.GameWin);
		}
	}


}
