using Godot;
using System;

public partial class PlayerDashState : Node {

    private Player playerNode;

    public override void _Ready() {
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta) {
        HandleSwitchStates();
    }

    private void HandleSwitchStates() {
        if (!playerNode.IsMoving()) {
            playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
        } else {
            playerNode.GetStateMachineNode().SwitchState<PlayerRunState>();
        }
    }

    public override void _Notification(int what) {
        base._Notification(what);

        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_DASH);
            SetPhysicsProcess(true);
        } else if (what == GameConstants.DISABLE_PREVIOUS_ANIMATION_STATE) {
            SetPhysicsProcess(false);
        }
    }
}

