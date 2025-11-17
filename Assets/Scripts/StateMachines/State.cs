using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class CustomSelectorAttribute : PropertyAttribute
{

}

[Serializable]
public abstract class State<T> : IState
{
    //protected readonly MonoBehaviour owner;
    //protected readonly Animator animator;

    protected T owner;

    public State()
    {
    }

    public void SetOwner(T owner)
    {
        if (this.owner == null)
        {
            this.owner = owner;
        }
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
