using Godot;
using System.Diagnostics;

public partial class PlayerIdleState : Node {

    private Player playerNode;

    public override void _Ready() {
        
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta) {
        playerNode.IsTryingToMove();
        if (playerNode.IsTryingToMove()) {
            playerNode.GetStateMachineNode().SwitchState<PlayerRunState>();
        }
    }

    public override void _Notification(int what) {
        base._Notification(what);
        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_IDLE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        } else if (what == GameConstants.DISABLE_PREVIOUS_ANIMATION_STATE) {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    public override void _Input(InputEvent @event) {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            playerNode.GetStateMachineNode().SwitchState<PlayerDashState>();
        }
    }
}
