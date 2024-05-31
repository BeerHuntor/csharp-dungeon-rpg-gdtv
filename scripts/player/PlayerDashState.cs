using Godot;
using System;

public partial class PlayerDashState : Node {

    private Player playerNode;
    [Export] private float dashSpeed = 2f; 
    [Export] private Timer dashTimer; 

    public override void _Ready() {
        playerNode = GetOwner<Player>();
        dashTimer.Timeout += HandleDashTimeoutEvent;
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta) {
        playerNode.MoveAndSlide(); //Does the actual moving 
    }
    private void HandleDashTimeoutEvent() {
        playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
        playerNode.Velocity = Vector3.Zero;
        SetPhysicsProcess(false);
    }
    /// <summary>
    /// Handles the setting of the players last known movement direction and applies a velocity * the given dashspeed.  
    /// </summary>
    private void HandleDashMovement() {
        playerNode.Velocity = playerNode.GetLastMoveDir();
        //player is idle
        if (!playerNode.IsMoving()) {
            playerNode.Velocity = new Vector3(playerNode.GetPlayerSpriteNode().Scale.X, 0, 0);
        }
        playerNode.Velocity *= dashSpeed;
    }
    /// <summary>
    /// Godots built in 'events' system.  Godot utlizies notifications similar to events for various things, we have hooked into this to serve our own purposes. 
    /// </summary>
    /// <param name="what"> (int) Notification number that is set from  the notification</param>
    public override void _Notification(int what) {
        base._Notification(what);

        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_DASH);
            dashTimer.Start();
            HandleDashMovement();
            SetPhysicsProcess(true);
        }
    }
}

