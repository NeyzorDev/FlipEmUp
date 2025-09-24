using System;


public interface ITransition
{
    IState ToState { get; }
    IPredicate ExitCondition { get; }
}
