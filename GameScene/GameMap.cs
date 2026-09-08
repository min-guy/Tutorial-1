using Godot;

public partial class GameMap : Node3D
{
	private DirectionalLight3D globalLight;

	private PackedScene collectibleScene;

	private Timer timer;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = new Timer();
		AddChild(timer);

		timer.Timeout += OnTimeout;

		timer.OneShot = false;
		timer.Start(5);

		globalLight = GetNode<DirectionalLight3D>(new NodePath("DirectionalLight3D"));
		
		collectibleScene = GD.Load<PackedScene>("uid://ckhvrjm8fgasq");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		globalLight.Rotate(new Vector3(0, 1, 0), (float)delta * 0.065f);
		globalLight.Rotate(new Vector3(1, 0, 0), (float)delta * 0.085f);
	}

	private void OnTimeout()
	{
		var collectible = collectibleScene.Instantiate<Collectible>();

		collectible.Position = new Vector3(GD.Randf() * 20.0f - 10, 1.5f, GD.Randf() * 20.0f - 10);

		AddChild(collectible);
	}
}
