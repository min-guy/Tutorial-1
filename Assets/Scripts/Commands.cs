using Godot;

public interface ICommand
{
    public void Execute();
}

public partial class EmptyCommand : ICommand
{
    public EmptyCommand() { }
    public void Execute() { }
}

public partial class MoveCommand : ICommand
{
    private Vector3 direction;
    private MovementComponent comp;

    public MoveCommand(Vector3 direction, MovementComponent comp)
    {
        this.direction = direction;
        this.comp = comp;
    }

    public void Execute()
    {
        comp.move(direction);
    }
}