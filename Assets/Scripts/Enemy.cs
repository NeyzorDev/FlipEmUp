using System;
using UnityEngine;


public class Enemy : Ship
{
    [Header("Behaviors")]
    [SerializeReference]
    private StandarEnemyMove stateMove = new StandarEnemyMove();
    [SerializeReference]
    private StandarEnemyShoot stateShoot = new StandarEnemyShoot();

    private StateMachine sm;

    public Enemy()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        InitializeSM();
    }

    private void InitializeSM()
    {
        sm = new StateMachine();

        stateMove.SetOwner(this);
        stateShoot.SetOwner(this);

        sm.AddState(stateMove);
        sm.AddState(stateShoot);
        
        sm.AddTransition(stateMove, stateShoot, new Predicate(stateMove.IsReadyToShoot));
        sm.AddTransition(stateShoot, stateMove, new Predicate(stateShoot.IsFinishedShooting));
        
        sm.SetInitialState(stateMove);
    }

    // Update is called once per frame
    public void Update()
    {
        sm.Update();
    }

    public override void Hit()
    {
        if(--hitPoints<= 0)
        {
            Destroy(gameObject);
        }
    }
}
