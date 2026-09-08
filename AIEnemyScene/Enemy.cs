using Godot;

public partial class Enemy : CharacterBody3D
{
	public const float Speed = 2.5f;
	public const float JumpVelocity = 4.5f;

	private NavigationAgent3D navAgent;

	[Export] private Player player;

	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent3D>(new NodePath("NavigationAgent3D"));
	}

	public override void _PhysicsProcess(double delta)
	{
		// retarget nav agent
		navAgent.TargetPosition = player.Position;
		
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get direction to move in based on where the navagent decides to go next.
		Vector3 direction = (navAgent.GetNextPathPosition() - this.Position).Normalized();
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

		// Move and slide, but also check if we collided with anything. 
		// 
		// If we did, then start to check what to see exactly what it was. 
		if (MoveAndSlide())
		{
			// Gets the GodotObject that last collided with this "enemy" in this frame.
			var obj = GetLastSlideCollision().GetCollider();

			if (obj is Player player)
			{
				GameSignals.Instance.EmitSignal(GameSignals.SignalName.GameLose);
			}
		}
	}
}
