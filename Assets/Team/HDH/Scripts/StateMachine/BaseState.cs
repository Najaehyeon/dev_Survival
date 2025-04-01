public abstract class BaseState
{
    public BaseStateMachine StateMachine { get; set; }

    protected BaseState(BaseStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
