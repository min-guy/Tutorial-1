using Godot;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private Label3D win_msg;

	public override void _Ready()
	{
		win_msg = GetNode<Label3D>(new NodePath("WinMsg"));
		win_msg.Visible = false;	

		GameSignals.Instance.GameWin += OnWin;
		GameSignals.Instance.GameLose += OnLose;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Cloning velocity so we can perform compontent-wise operations (such as assigning to .X, .Y, .Z on it).
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			// Slowly decelerate our current velocity such that stopping isn't so sudden or feels wrong
			// to the player.
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		// Apply our newly calculated velcoity to the internal "Velocity" field of the CharacterBody3D. 
		// This tells Godot's built-in method for this physics body how to move for this physics frame.
		Velocity = velocity;

		// Perform the physics calculation, moving and sliding the character body forward.
		MoveAndSlide();
	}

	private void OnWin()
	{
		win_msg.Text = "You Win!";
		win_msg.Visible = true;

		// Force the player high up. Hopefully, this should make it extremely clear the player has won
		// and should also prevent the player from accidentally dying whilst also still winning at the same time. 
		Position = new Vector3(Position.X, 10000, Position.Z);
	}

	private void OnLose()
	{
		win_msg.Text = "You Lose!";
		win_msg.Visible = true;

		Node3D mesh = GetNode<Node3D>(new NodePath("MeshInstance3D"));
		mesh.Visible = false;

		// Disable all relevant processing methods for the player. Prevents them from performing any more actions once losing.
		SetProcessInput(false);
		SetProcess(false);
		SetPhysicsProcess(false);
	}
}
