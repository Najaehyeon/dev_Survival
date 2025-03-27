public abstract class BaseState
{
    protected GameStateMachine stateMachine;

    public BaseState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
