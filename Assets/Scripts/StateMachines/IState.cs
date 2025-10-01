using System;
using UnityEngine;


public interface IState
{
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();

}
