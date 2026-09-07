using Godot;

public partial class GameSignals : Node
{
    public static GameSignals Instance { get; private set; }

    [Signal]
    public delegate void OnHitCollectibleEventHandler();

    [Signal]
    public delegate void GameWinEventHandler();
    
    public override void _Ready()
    {
        Instance = this;
    }
}