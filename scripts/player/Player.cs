using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;

public partial class Player : CharacterBody3D{

    [Export] private float moveSpeed = 3f;

    [ExportGroup("Nodes")] 
    [Export] private Sprite3D spriteNode;
    [Export] private AnimationPlayer animationPlayerNode;

    private Vector3 lastMoveDir; 

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
    ///<summary>
    /// Gets the input vector normalized based on keypresses that match the input map. 
    ///</summary>
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

    ///<summary>
    /// Flips the sprite when the input vector is non zero. So the sprite always faces the last known moveDir.X 
    ///</summary>
    private void FlipSpriteFacingDirection(Vector3 moveDir) {
        if (moveDir != Vector3.Zero) {
            lastMoveDir = moveDir;
            spriteNode.FlipH = lastMoveDir.X < 0; 
        }
    }

    ///<summary>
    /// Handles all the animation playback calls. 
    ///</summary>
    private void HandleAnimationPlayback() {
        // Ternary opperator which checks if IsMoving is true, it plays run, else idle. 
        // animationPlayerNode.Play(IsMoving() ? GameConstants.ANIMATION_PLAYER_RUN : GameConstants.ANIMATION_PLAYER_IDLE);
    }

    public bool IsMoving() {
        return !Velocity.IsZeroApprox();
    }
    ///<summary>
    /// Handles the player movement and is responsible for moving the player. 
    ///</summary>
    private void HandleMovement() {
        Velocity = new Vector3(GetMovementVectorNormalized().X, 0, GetMovementVectorNormalized().Y);

        Velocity *= moveSpeed;
        MoveAndSlide();
    }

    public Sprite3D GetPlayerSpriteNode() {
        return spriteNode;
    }

    public AnimationPlayer GetPlayerAnimationPlayer() {
        return animationPlayerNode;
    }
}