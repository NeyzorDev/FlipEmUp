using System;


public interface ITransition
{
    public IState ToState { get; }
    public Func<bool> ExitCondition { get; }
}

public class Transition : ITransition
{
    public IState ToState { get; }
    public Func<bool> ExitCondition { get; }

    public Transition(IState to, Func<bool> exit)
    {
        ToState = to;
        ExitCondition = exit;
    }
}
