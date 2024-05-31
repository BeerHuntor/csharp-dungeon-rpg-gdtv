using DungeonRPGGMTVC.scripts.player;
using Godot;
using System.Diagnostics;

public partial class PlayerIdleState : PlayerStateBase {

    public override void _PhysicsProcess(double delta) {
        playerNode.IsTryingToMove();
        if (playerNode.IsTryingToMove()) {
            playerNode.GetStateMachineNode().SwitchState<PlayerRunState>();
        }
    }

    public override void _Input(InputEvent @event) {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            playerNode.GetStateMachineNode().SwitchState<PlayerDashState>();
        }
    }

    protected  override void EnterState() {
        base.EnterState();
        
        playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_IDLE);
    }
}
