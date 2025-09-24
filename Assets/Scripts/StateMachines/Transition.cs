using System;


public class Transition : ITransition
{
    public IState ToState { get; }
    public IPredicate ExitCondition { get; }

    public Transition(IState to, IPredicate exit)
    {
        ToState = to;
        ExitCondition = exit;
    }
}
