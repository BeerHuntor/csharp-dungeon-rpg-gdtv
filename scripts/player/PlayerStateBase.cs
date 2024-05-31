namespace DungeonRPGGMTVC.scripts.player; 
using Godot; 

public partial class PlayerStateBase : Node {

    protected Player playerNode;

    public override void _Ready() {
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(true);
        SetProcessInput(true);
    }
    /// <summary>
    /// Godots built in 'events' system.  Godot utlizies notifications similar to events for various things, we have hooked into this to serve our own purposes. 
    /// </summary>
    /// <param name="what"> (int) Notification number that is set from  the notification</param>
    public override void _Notification(int what) {
        if (what == GameConstants.SWITCH_ANIMATION_STATE) {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        } else if (what == GameConstants.DISABLE_PREVIOUS_ANIMATION_STATE) {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }
    
    /// <summary>
    /// Our own virtual method to be overridden in the state classes. 
    /// </summary>
    protected virtual void EnterState() {
        
    }
}
