using Godot;

public partial class Player : CharacterBody3D {

	[Export] private float moveSpeed = 3f; 

	[ExportGroup("Nodes")]
	[Export] private Sprite3D spriteNode; 
	[Export] private AnimationPlayer animationPlayerNode;

	//Called when a node is ready. (godot's start())
    public override void _Ready(){
        animationPlayerNode.Play(GameConstants.ANIMATION_PLAYER_RUN);
    }
	// Called every frame
    public override void _Process(double delta){
		
    }
    //Called everytime a physics frame is called. 
    public override void _PhysicsProcess(double delta){
		Velocity = new(GetMovementVectorNormalized().X, 0, GetMovementVectorNormalized().Y);
		
		if (Velocity == Vector3.Zero){
			animationPlayerNode.Play(GameConstants.ANIMATION_PLAYER_IDLE);
		} else {

			animationPlayerNode.Play(GameConstants.ANIMATION_PLAYER_RUN);
			Velocity *= moveSpeed;
			//Which direction are we moving
			if (Velocity.X > 0){ 
				//were moving right
				spriteNode.Scale = new Vector3(1, 1, 1);
			} else { 
				//were moving left
				spriteNode.Scale = new Vector3(-1, 1, 1);
			}
			MoveAndSlide();

		}
	}

	public override void _Input(InputEvent @event){
	}

	private Vector2 GetMovementVectorNormalized() {
		Vector3 moveDir = Vector3.Zero;

		if(Input.IsActionPressed(GameConstants.INPUT_MOVE_UP)){
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
}
