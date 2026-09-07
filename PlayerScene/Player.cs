using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private Label3D win_msg;

	public override void _Ready()
	{
		win_msg = GetNode<Label3D>(new NodePath("WinMsg"));
		win_msg.Visible = false;	

		GameSignals.Instance.Connect(GameSignals.SignalName.GameWin, Callable.From(OnWin));
	}

	public override void _PhysicsProcess(double delta)
	{
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
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		if (MoveAndSlide())
		{
			var collisionShape = GetLastSlideCollision();
			var obj = collisionShape.GetCollider();

			if (obj is Node3D node)
			{
				OnBodyEntered(node);
			}
		}
	}

	private void OnWin()
	{
		win_msg.Text = "You Win!";
		win_msg.Visible = true;
	}

	private void OnLose()
	{
		win_msg.Text = "You Lose!";
		win_msg.Visible = true;

		Node3D mesh = GetNode<Node3D>(new NodePath("MeshInstance3D"));
		mesh.Visible = false;

		// Disable all relevant methods for the player.
		SetProcessInput(false);
		SetProcess(false);
		SetPhysicsProcess(false);
	}

	private void OnBodyEntered(Node3D node)
	{
		if (node.IsInGroup("Collectible"))
		{
			node.Visible = false;
			node.QueueFree();

			GameSignals.Instance.EmitSignal(GameSignals.SignalName.OnHitCollectible);
			
		}

		if (node.IsInGroup("Enemy"))
		{
			OnLose();	
		}
	}
}
