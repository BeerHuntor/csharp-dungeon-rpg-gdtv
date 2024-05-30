using Godot;
using System;

public partial class AnimationStateMachine : Node {

    [Export] private Node currentState;
    [Export] private Node[] stateArray;

    private Player player; 

    public override void _Ready() {
        player = GetOwner<Player>();
        currentState.Notification(GameConstants.SWITCH_ANIMATION_STATE);        
    }

    public override void _Process(double delta) {
        
    }

}