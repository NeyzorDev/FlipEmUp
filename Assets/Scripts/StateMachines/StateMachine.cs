using System;
using System.Collections.Generic;


public class StateMachine
{
    private StateNode current;
    private Dictionary<Type, StateNode> nodes;
    public HashSet<ITransition> globalTransitions;

    public StateMachine() 
    {
        nodes = new Dictionary<Type, StateNode>();
    }

    public void Update()
    {
        current.state.Update();

        foreach (Transition t in globalTransitions)
        {
            if (t.ExitCondition.Evaluate())
            {
                ChangeState(t.ToState);
            }
        }

        foreach (Transition t in current.transitions)
        {
            if(t.ExitCondition.Evaluate())
            {
                ChangeState(t.ToState);
            }
        }
    }

    public void AddState(IState state)
    {
        nodes.Add(state.GetType(), new StateNode(state));
    }

    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        nodes[from.GetType()].AddTransition(to, condition);
    }
    public void AddGlobalTransition(IState to, IPredicate condition)
    {
        globalTransitions.Add(new Transition(to, condition));
    }

    private void ChangeState(IState newState)
    {
        current.state.OnExit();
        current = nodes[newState.GetType()];
        current.state.OnStart();
    }
}
