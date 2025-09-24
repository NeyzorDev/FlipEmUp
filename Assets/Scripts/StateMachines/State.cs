using UnityEngine;


public abstract class State : IState
{
    protected readonly GameObject owner;
    //protected readonly Animator animator;

    public State(GameObject owner)
    {
        this.owner = owner;
    }

    public virtual void OnExit()
    {
        // NotImplemented
    }

    public virtual void OnStart()
    {
        // NotImplemented
    }

    public virtual void Update()
    {
        // NotImplemented
    }
}
