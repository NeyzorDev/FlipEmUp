using System;
using UnityEngine;


[Serializable]
public class serTest
{
    public bool isTest = true;
    [SerializeField]
    private float speed = 2.3f;
    [SerializeField]
    private Vector3 pos = new Vector3(0.0f, 2.0f, 1.0f);
    [SerializeField]
    private float angle = 22.2f;
}

public class Enemy : Ship
{
    [Header("Serializable Tests")]
    [SerializeField]
    private serTest test1 = null;
    [SerializeReference]
    private serTest test2 = null;
    public serTest test3 = null;

    [Header("Behaviors")]
    [SerializeField]
    private StandarEnemyMove stateMove = null;
    [SerializeField]
    private StandarEnemyShoot stateShoot = null;

    private StateMachine sm;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        InitializeSM();
    }

    private void InitializeSM()
    {
        sm = new StateMachine();
        stateMove = new StandarEnemyMove(this);
        stateShoot = new StandarEnemyShoot(this);
        
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
