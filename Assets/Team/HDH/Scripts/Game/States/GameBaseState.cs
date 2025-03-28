public abstract class GameBaseState : BaseState
{
    protected GameStateMachine GameStateMachine;

    protected GameBaseState(GameStateMachine stateMachine) : base(stateMachine)
    {
        GameStateMachine = stateMachine;
    }
}
