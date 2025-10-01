using System;
using UnityEngine;


[Serializable]
public abstract class State : IState
{
    //protected readonly MonoBehaviour owner;
    //protected readonly Animator animator;

    public State(/*MonoBehaviour owner*/)
    {
        //this.owner = owner;
    }

    public virtual void OnEnter()
    {
        // NotImplemented
    }

    public virtual void Update()
    {
        // NotImplemented
    }

    public virtual void FixedUpdate()
    {
        // NotImplemented
    }

    public virtual void OnExit()
    {
        // NotImplemented
    }
}
