using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bState
{
    public Action OnEnter;
    public Action Update;
    public Action OnExit;

    public static bState Create(Action enter, Action update, Action exit)
    {
        bState state = new bState();

        state.OnEnter = enter;
        state.Update = update;
        state.OnExit = exit;

        return state;
    }
}


class KamikazeEnemy : Ship
{
    /*[Header("Behaviors")]
    [SerializeReference]
    private KamikazeRePosition stateReposition = new KamikazeRePosition();
    [SerializeReference]
    private KamikazeTargetLock stateTargetLock = new KamikazeTargetLock();
    [SerializeReference]
    private KamikazeAttack stateAttack = new KamikazeAttack();
    */
    private float speed;

    [Header("Attack")]
    [SerializeReference]
    private float maxSpeed = 100.0f;
    [SerializeReference]
    private float acceleration = 120.0f;
    [SerializeReference]
    private float angle = 10;

    //private StateMachine sm;

    // ----

    private GameObject player;
    private float velocity;

    private enum actionState
    {
        None,
        Attack,
        Brake,
        Retarget
    }

    bState Attack;
    
    private actionState action;
    private actionState prevAction;

    public void Start()
    {
        prevAction = actionState.None;
        action = actionState.Retarget;


        Attack = bState.Create(AttackEnter ,AttackUpdate, AttackExit);
    }

    // Update is called once per frame
    public void Update()
    {
        player = FindFirstObjectByType<Player>().gameObject;
        if (player == null)
        {
            // Set state to idle
            velocity = 0.0f;
            return;
        }


        if (action != prevAction)
        {
            if (action == actionState.Attack) { AttackEnter(); }
            else if (action == actionState.Brake) {/* AttackEnter() */; }
            else if (action == actionState.Retarget) { /* AttackEnter()*/ ; }
        }
        prevAction = action;

        if (action == actionState.Attack)
        {
            AttackUpdate();
        }
        else if (action == actionState.Brake)
        {

        }
        else if (action == actionState.Retarget)
        {

        }
    }

    //Estados Acceleration, Deceleration, Reposition.
    private void AttackEnter()
    {

    }

    private void AttackUpdate()
    {
        float dt = Time.deltaTime;

        velocity += acceleration * dt;
        if (velocity > maxSpeed) { velocity = maxSpeed; }

        transform.position = transform.forward * velocity * dt;


        //Exit Condition.
        if (IsDodged())
        {
            // Swap state
            action = actionState.Brake;
            AttackExit();
        }
    }
    private bool IsDodged()
    {
        //Exit Condition.
        return Vector3.Dot(transform.forward, Vector3.Normalize(player.transform.position - transform.position)) > 0.0f;
    }

    private void AttackExit()
    {

    }


    public override void Hit()
    {
        if (--hitPoints <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hit();
            Hit();
        }
    }
}

