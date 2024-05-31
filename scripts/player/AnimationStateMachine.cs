using Godot;

public partial class AnimationStateMachine : Node {

    [Export] private Node currentState;
    [Export] private Node[] stateArray;

    private Node stateToSwitch;

    public override void _Ready() {
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

        currentState.Notification(GameConstants.DISABLE_PREVIOUS_ANIMATION_STATE);
        currentState = stateToSwitch;
        currentState.Notification(GameConstants.SWITCH_ANIMATION_STATE);
    }

}
