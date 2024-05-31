using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;

public partial class Player : CharacterBody3D{

    [Export] private float moveSpeed = 3f;

    [ExportGroup("Nodes")] 
    [Export] private Sprite3D spriteNode;
    [Export] private AnimationPlayer animationPlayerNode;
    [Export] private AnimationStateMachine animationStateMachine;

    private Vector3 lastMoveDir;
    private Vector3 moveDir;

    //Called when a node is ready. (godot's start())
    public override void _Ready() {
    }

    // Called every frame
    public override void _Process(double delta) {
    }

    //Called everytime a physics frame is called. 
    public override void _PhysicsProcess(double delta) {
        GetInputVectorNormalized();
        FlipSpriteFacingDirection(Velocity);
    }
    ///<summary>
    /// Gets the input vector normalized based on keypresses that match the input map. 
    ///</summary>
    public Vector2 GetInputVectorNormalized() {
        moveDir = Vector3.Zero;

        if (Input.IsActionPressed(GameConstants.INPUT_MOVE_UP)) {
            moveDir.Y = -1;
        }

        if (Input.IsActionPressed(GameConstants.INPUT_MOVE_DOWN)) {
            moveDir.Y = 1;
        }
        
        if (Input.IsActionPressed(GameConstants.INPUT_MOVE_LEFT)) {
            moveDir.X = -1;
        }

        if (Input.IsActionPressed(GameConstants.INPUT_MOVE_RIGHT)) {
            moveDir.X = 1;
        }

        if (moveDir.Length() > 0) {
            //We are moving
            moveDir = moveDir.Normalized();
        }

        return new Vector2(moveDir.X, moveDir.Y);
    }

    ///<summary>
    /// Flips the sprite when the input vector is non zero. So the sprite always faces the last known moveDir.X 
    ///</summary>
    private void FlipSpriteFacingDirection(Vector3 moveDir) {
        if (moveDir != Vector3.Zero) {
            lastMoveDir = moveDir;
            spriteNode.FlipH = lastMoveDir.X < 0; 
        }
    }
    public bool IsTryingToMove() {
        return !GetInputVectorNormalized().IsZeroApprox();
    }
    public bool IsMoving() {
        return !Velocity.IsZeroApprox();
    }
    ///<summary>
    /// Handles the player movement and is responsible for moving the player. 
    ///</summary>
    private void HandleMovement() {
        Velocity = new Vector3(GetInputVectorNormalized().X, 0, GetInputVectorNormalized().Y);

        Velocity *= moveSpeed;
        MoveAndSlide();
    }
    ///<summary>
    /// Returns the Animation Player Node attached to the player
    ///</summary>
    public AnimationPlayer GetPlayerAnimationPlayer() {
        return animationPlayerNode;
    }
    ///<summary>
    /// Returns the AnimationStateMachine object
    ///</summary>
    public AnimationStateMachine GetStateMachineNode() {
        return animationStateMachine; 
    }
    ///<summary>
    /// Returns the movement speed of the player
    ///</summary>
    public float GetMovementSpeed() {
        return moveSpeed;
    }
}