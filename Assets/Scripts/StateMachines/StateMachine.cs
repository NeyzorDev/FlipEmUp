using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StateMachine
{
    private class StateNode
    {
        public State state { get; }
        public HashSet<ITransition> Transitions { get; }

        public StateNode(State state)
        {
            this.state = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(State s, Func<bool> p)
        {
            Transitions.Add(new Transition(s, p));
        }
    }

    private StateNode previous;
    private StateNode current;
    private Dictionary<String, StateNode> nodes;
    //private HashSet<ITransition> globalTransitions;

    public StateMachine()
    {
        previous = null;
        current = null;

        nodes = new Dictionary<String, StateNode>();
        //globalTransitions = new HashSet<ITransition>();
    }

    public void Update()
    {
        Assert.IsNotNull(current, "StateMachine has not any current state selected.");

        // On Enter State.
        if (previous == null || current != previous)
        {
            current.state.OnEnter();
        }

        previous = current;

        // Update State.
        current.state.Update();

        //Evaluating Exit Conditions.
        foreach (ITransition t in current.Transitions)
        {
            if (t.ExitCondition())
            {
                ChangeState(t.ToState);
            }
        }

        // On Exit State
        if (current != previous)
        {
            previous.state.OnExit();
        }
    }

    public State CreateState(String name, Action enter, Action update, Action exit)
    {
        State action = new State(name);

        action.OnEnter = enter;
        action.Update = update;
        action.OnExit = exit;

        nodes[name] = new StateNode(action);

        return action;
    }

    public void AddTransition(State from, State to, Func<bool> condition)
    {
        nodes[from.name].AddTransition(to, condition);
    }

    /*public void AddGlobalTransition(IState to, Func<bool> condition)
    {
        globalTransitions.Add(new Transition(to, condition));
    }*/

    public void SetInitialState(IState newState)
    {
        current = nodes[newState.name];

        GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current State: " + newState.name;
    }

    private void ChangeState(IState newState)
    {
        current = nodes[newState.name];

        GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current State: " + newState.name;
    }
}
