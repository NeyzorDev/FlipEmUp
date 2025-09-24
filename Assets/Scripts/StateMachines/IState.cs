using System;



public interface IState
{
    void OnStart();
    void Update();
    void OnExit();

}
