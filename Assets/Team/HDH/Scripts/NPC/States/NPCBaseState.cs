public abstract class NPCBaseState : BaseState
{
    protected NPCStateMachine NPCStateMachine;

    protected NPCBaseState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        NPCStateMachine = stateMachine;
    }
}
