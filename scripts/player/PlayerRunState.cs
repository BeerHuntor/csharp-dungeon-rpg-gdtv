using Godot;
using System;

public partial class PlayerRunState : Node {

    private Player playerNode; 
    public override void _Ready() { 
        
        playerNode = GetOwner<Player>();
    }

    public override void _PhysicsProcess(double delta) {
        if (playerNode.IsMoving()) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_RUN);
        } 
    }

    public override void _Notification(int what) {
        base._Notification(what);
        
        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_RUN);            
        }
    }
}