using Godot;
using System;

public partial class PlayerRunState : Node {

    private Player playerNode;
    public override void _Ready() { 
        
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta) {
        if (!playerNode.IsMoving()) {
            playerNode.GetStateMachineNode().SwitchState<PlayerIdleState>();
        }
        HandleMovement();
    }
    
    ///<summary>
    /// Handles the player movement and is responsible for moving the player. 
    ///</summary>
    private void HandleMovement() {
        playerNode.Velocity = new Vector3(playerNode.GetInputVectorNormalized().X, 0, playerNode.GetInputVectorNormalized().Y);

        playerNode.Velocity *= playerNode.GetMovementSpeed();
        playerNode.MoveAndSlide();
    }

    public override void _Notification(int what) {
        base._Notification(what);
        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_RUN);
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