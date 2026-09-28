using Godot;

[GlobalClass]
public partial class MovementComponent : Node
{
    [Export] float speed;
    [Export] RigidBody3D rb;

    private float delta;

    public override void _Process(double delta)
    {
        this.delta = (float)delta;
    }

    public void move(Vector3 direction)
    {
        rb.MoveAndCollide(direction * speed * delta);
    }
}