public abstract class BaseState : IState
{
    public IStateMachine StateMachine { get; set; }

    protected BaseState(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
