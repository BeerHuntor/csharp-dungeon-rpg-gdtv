using System.Runtime.InteropServices;
using Godot;

public partial class Player : CharacterBody3D{

    [Export] private float moveSpeed = 3f;

    [ExportGroup("Nodes")] 
    [Export] private Sprite3D spriteNode;
    [Export] private AnimationPlayer animationPlayerNode;

    //Called when a node is ready. (godot's start())
    public override void _Ready() {
        animationPlayerNode.Play(GameConstants.ANIMATION_PLAYER_RUN);
    }

    // Called every frame
    public override void _Process(double delta) {
    }

    //Called everytime a physics frame is called. 
    public override void _PhysicsProcess(double delta) {
        HandleAnimationPlayback();
        FlipSpriteFacingDirection(Velocity);
        HandleMovement();
    }

    private Vector2 GetMovementVectorNormalized() {
        Vector3 moveDir = Vector3.Zero;

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

    private void FlipSpriteFacingDirection(Vector3 moveDir) {
        spriteNode.FlipH = moveDir.X < 0;
    }
    private void HandleAnimationPlayback() {
        // Ternary opperator which checks if IsMoving is true, it plays run, else idle. 
        animationPlayerNode.Play(IsMoving() ? GameConstants.ANIMATION_PLAYER_RUN : GameConstants.ANIMATION_PLAYER_IDLE);
    }

    private bool IsMoving() {
        return !Velocity.IsZeroApprox();
    }
    
    private void HandleMovement() {
        Velocity = new Vector3(GetMovementVectorNormalized().X, 0, GetMovementVectorNormalized().Y);

        Velocity *= moveSpeed;
        MoveAndSlide();
    }
}