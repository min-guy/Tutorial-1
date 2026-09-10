using Godot;

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
		
		GameSignals.Instance.OnHitCollectible += IncreaseCounter;
	}

	private void IncreaseCounter(Node3D _, Node3D collectible)
	{
		// Hide the node and then remove it from the scene.
		collectible.Visible = false;
		collectible.QueueFree();
		
		// Simply increment the count and display it.
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
