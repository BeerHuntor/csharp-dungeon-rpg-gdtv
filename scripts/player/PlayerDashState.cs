using Godot;
using System;

public partial class PlayerDashState : Node {

    private Player playerNode;
    [Export] private Timer dashTimer; 

    public override void _Ready() {
        playerNode = GetOwner<Player>();
        dashTimer.Timeout += HandleDashTimeoutEvent;
    }
    private void HandleDashTimeoutEvent() {
        playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
    }
    
    public override void _Notification(int what) {
        base._Notification(what);

        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_DASH);
            dashTimer.Start();
        }
    }
}

