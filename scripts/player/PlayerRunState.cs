using Godot;
using System;

public partial class PlayerRunState : Node {

    private Player playerNode; 
    public override void _Ready() { 
        
        playerNode = GetOwner<Player>();
        playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_IDLE);

        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta) {
        if (playerNode.GetMoveDir() == Vector3.Zero) {
            playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
        } 
    }

    public override void _Notification(int what) {
        base._Notification(what);
        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_RUN);
            SetPhysicsProcess(true);
        } else if (what == GameConstants.DISABLE_PREVIOUS_ANIMATION_STATE) {
            SetPhysicsProcess(false);
        }
    }
}