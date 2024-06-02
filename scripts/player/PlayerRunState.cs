using DungeonRPGGMTVC.scripts.player;
using Godot;
using System;

public partial class PlayerRunState : PlayerStateBase {

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

	public override void _Input(InputEvent @event) {
		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
			playerNode.GetStateMachineNode().SwitchState<PlayerDashState>();
		}
	}

	protected override void EnterState() {
		base.EnterState();
		
		playerNode.GetPlayerAnimationPlayer().Play(GameConstants.ANIMATION_PLAYER_RUN);
	}
}
