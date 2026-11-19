using System;
using UnityEngine;

/*[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class CustomSelectorAttribute : PropertyAttribute
{

}*/

public interface IState
{
    public String name { get; }
}

[Serializable]
public struct State : IState
{
    public static Action VoidAction = () => { };

    public String name { get; }

    public Action OnEnter;
    public Action Update;
    public Action FixedUpdate;
    public Action OnExit;

    public State(String name)
    {
        this.name = name;
        OnEnter = VoidAction;
        Update = VoidAction;
        FixedUpdate = VoidAction;
        OnExit = VoidAction;
    }
}

