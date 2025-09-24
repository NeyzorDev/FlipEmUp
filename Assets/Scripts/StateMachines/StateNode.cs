using System;
using System.Collections.Generic;


public class StateNode
{
    public IState state;
    public HashSet<ITransition> transitions;

    public StateNode(IState state)
    {
        this.state = state;
        transitions = new HashSet<ITransition>();
    }

    public void AddTransition(IState s, IPredicate p)
    {
        transitions.Add(new Transition(s, p));
    }
}
