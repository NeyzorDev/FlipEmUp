using System;
using System.Collections.Generic;
using UnityEngine;




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
    private float acceleration = 120.0f; //50.0f
    [SerializeReference]
    private float angle = 10;

    // ----

    private GameObject player;
    private float velocity;

    Rigidbody2D phy;

    private StateMachine sm;


    public void Start()
    {
        phy = GetComponent<Rigidbody2D>();

        InitializeSM();
    }
    private void InitializeSM()
    {
        sm = new StateMachine();

        // Define states.
        State attack = sm.CreateState("Attack", AttackEnter, AttackUpdate, AttackExit);
        State brake = sm.CreateState("Brake", Idle, Idle, Idle);
        
        // Define transitions.
        sm.AddTransition(attack, brake, IsAttackDodged);
        sm.SetInitialState(attack);
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

        sm.Update();
    }

    //Attack, Brake, Retarget.
    private void AttackEnter()
    {

    }

    private void AttackUpdate()
    {
        float dt = Time.deltaTime;

        velocity += acceleration * dt;
        if (velocity > maxSpeed) { velocity = maxSpeed; }

        transform.position = transform.forward * velocity * dt;

        //phy.AddForce(new Vector2(aceleration, 0.0f), ForceMode2D.Force);
    }

    // Attack Exit Condition
    private bool IsAttackDodged()
    {
        //Exit Condition.
        return Vector3.Dot(transform.forward, Vector3.Normalize(player.transform.position - transform.position)) < 0.0f;
    }

    private void AttackExit()
    {

    }

    private void Idle() { }

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

