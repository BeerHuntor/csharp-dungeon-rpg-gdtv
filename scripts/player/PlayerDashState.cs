using DungeonRPGGMTVC.scripts.player;
using Godot;
using System;

public partial class PlayerDashState : PlayerStateBase {

    [Export] private float dashSpeed = 2f; 
    [Export] private Timer dashTimer; 

    public override void _Ready() {
        base._Ready();
        dashTimer.Timeout += HandleDashTimeoutEvent;
    }

    public override void _PhysicsProcess(double delta) {
        playerNode.MoveAndSlide(); //Does the actual moving 
    }
    private void HandleDashTimeoutEvent() {
        playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
        playerNode.Velocity = Vector3.Zero;
    }
    /// <summary>
    /// Handles the setting of the players last known movement direction and applies a velocity * the given dash speed.  
    /// </summary>
    private void HandleDashMovement() {
        playerNode.Velocity = playerNode.GetLastMoveDir();
        
        // Is player Idle.
        if (!playerNode.IsMoving()) {
            playerNode.Velocity = new Vector3(playerNode.GetPlayerSpriteNode().Scale.X, 0, 0);
        }
        
        playerNode.Velocity *= dashSpeed;
    }
    
    protected override void EnterState() {
        base.EnterState();
        
        playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_DASH);
        dashTimer.Start();
        HandleDashMovement();
    }
}

