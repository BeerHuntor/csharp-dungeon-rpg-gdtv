using Godot;
using System;

public partial class AnimationStateMachine : Node {

    [Export] private Node currentState;
    [Export] private Node[] stateArray;

    private Player player;
    private Node stateToSwitch; 

    public override void _Ready() {
        player = GetOwner<Player>();
        currentState.Notification(GameConstants.SWITCH_ANIMATION_STATE);        
    }

    public override void _Process(double delta) {
        
    }

    public void SwitchState<T>() {

        stateToSwitch = null; 
        
        foreach (Node state in stateArray) {
            if (state is T) {
                stateToSwitch = state; 
            }
        }
        //We haven't found a state in our StateMachine. 
        if (stateToSwitch == null) { return; }

        currentState = stateToSwitch;
        currentState.Notification(GameConstants.SWITCH_ANIMATION_STATE);
    }

}