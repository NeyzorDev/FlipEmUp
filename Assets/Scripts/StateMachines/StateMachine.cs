using System;
using System.Collections.Generic;


public class StateMachine
{
     private class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> Transitions { get; }

        public StateNode(IState state)
        {
            this.State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState s, IPredicate p)
        {
            Transitions.Add(new Transition(s, p));
        }
    }

    private StateNode current;
    private Dictionary<Type, StateNode> nodes;
    private HashSet<ITransition> globalTransitions;

    public StateMachine() 
    {
        nodes = new Dictionary<Type, StateNode>();
        globalTransitions = new HashSet<ITransition>();
    }

    public void Update()
    {
        current.State.Update();

        foreach (ITransition t in globalTransitions)
        {
            if (t.ExitCondition.Evaluate())
            {
                ChangeState(t.ToState);
            }
        }

        foreach (ITransition t in current.Transitions)
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
    public void SetInitialState(IState newState)
    {
        current = nodes[newState.GetType()];
        current.State.OnEnter();
    }

    private void ChangeState(IState newState)
    {
        current.State.OnExit();
        current = nodes[newState.GetType()];
        current.State.OnEnter();
    }
}
