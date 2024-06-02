using DungeonRPGGMTVC.scripts.player;
using Godot;
using System;

public partial class PlayerDashState : PlayerStateBase {

    [Export] private float dashSpeed = 2f; 
    [Export] private Timer dashTimer;

    private bool isDashing; 

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
        isDashing = false; 
    }
    /// <summary>
    /// Handles the setting of the players last known movement direction and applies a velocity * the given dash speed.  
    /// </summary>
    private void HandleDashMovement() {
        if (isDashing){ return; }
        
        playerNode.Velocity = playerNode.GetLastMoveDir();
        
        // Is player Idle.
        if (!playerNode.IsMoving()) {
            playerNode.Velocity = new Vector3(playerNode.GetPlayerSpriteNode().Scale.X, 0, 0);
            return;
        }

        playerNode.Velocity = (new Vector3(playerNode.Velocity.X, 0, playerNode.Velocity.Y) * dashSpeed);
    }
    
    protected override void EnterState() {
        base.EnterState();
        
        playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_DASH);
        isDashing = true; 
        dashTimer.Start();
        HandleDashMovement();
    }
}

