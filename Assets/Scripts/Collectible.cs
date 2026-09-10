using Godot;

public partial class Collectible : Area3D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotate(new Vector3(0, 1, 0), (float)delta * 1.5f);
		Rotate(new Vector3(0, 0, 1), (float)delta);
	}

	private void OnBodyEntered(Node3D node)
	{
		// Filter out any other intersections that may occur that cannot pickup the collectible.
		if (!node.IsInGroup("CanCollect")) return;

		// Send a single that the player and UI can listen to.
		GameSignals.Instance.EmitSignal(GameSignals.SignalName.OnHitCollectible, node, this);
	}
}
