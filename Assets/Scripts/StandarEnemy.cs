using System;
using System.Threading;
using UnityEngine;


public class StandarEnemy : Ship
{
    /*[Header("Behaviors")]
    [SerializeReference]
    private StandarEnemyMove stateMove = new StandarEnemyMove();
    [SerializeReference]
    private StandarEnemyShoot stateShoot = new StandarEnemyShoot();
    */
    [Header("Move")]
    [SerializeField]
    private float speed = 15.0f;
    [SerializeField]
    private Vector3 offsetPosition = new Vector3(4.0f, 0.0f, 0.0f);
    [SerializeField]
    private float angleOffset = 30.0f;

    [Header("Shoot")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shootTime = 0.5f;
    [SerializeField]
    private float bulletSpeed = 25.0f;
    [SerializeField]
    private float bulletLiveTime = 4.0f;
    [SerializeField]
    private int bulletsPerCicle = 3;


    private Vector3 originalPos;
    private Vector3 targetPos;
    private Vector3 currOffsetPos;

    private Vector3 velocity;

    private Timer shootTimer;
    private int bullets;

    private Vector3 targetNormal;


    private StateMachine sm;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        currOffsetPos = offsetPosition;

        player = GameObject.FindGameObjectWithTag("Player");

        InitializeSM();
    }

    private void InitializeSM()
    {
        sm = new StateMachine();

        // Define states.
        State move = sm.CreateState("Move", MoveEnter, MoveUpdate, MoveExit);
        State shoot = sm.CreateState("Shoot", ShootEnter, ShootUpdate, ShootExit);
        
        // Define transitions.
        sm.AddTransition(move, shoot, IsReadyToShoot);
        sm.AddTransition(shoot, move, IsFinishedShooting);

        sm.SetInitialState(move);

    }

    // Update is called once per frame
    public void Update()
    {
        sm.Update();
    }


    public void MoveEnter()
    {
        // Select where and how is going to move.
        originalPos = transform.position;
        targetPos = player.transform.position + currOffsetPos;
        
        velocity = targetPos - originalPos;
        velocity.Normalize();
        velocity *= speed;

        targetNormal = targetPos - originalPos;
        targetNormal.Normalize();
    }

    public void MoveUpdate()
    {
        float dt = Time.deltaTime;

        Vector3 deltaVel = velocity * dt;
        transform.position += deltaVel;
    }

    public bool IsReadyToShoot()
    {
        Vector3 targetDistance = targetPos - transform.position;
        targetDistance.Normalize();

        return (Vector3.Dot(targetDistance, targetNormal) < 0.0f);
    }

    public void MoveExit()
    {
        // Adjust offset target position.
        Quaternion offsetRotation = Quaternion.AngleAxis(angleOffset, new Vector3(0.0f, 0.0f, 1.0f));
        currOffsetPos = offsetRotation * currOffsetPos;
    }

    public void ShootEnter()
    {
        shootTimer = new Timer(shootTime, ShootReload);

        bullets = bulletsPerCicle;
        shootTimer.Play();
    }

    public void ShootUpdate()
    {
        shootTimer.Update();
    }

    public bool IsFinishedShooting()
    {
        return bullets <= 0;
    }

    public void ShootExit()
    {
    }

    private void Fire()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetShoot = (p) ? p.transform.position - transform.position : new Vector3(1, 0, 0);
        targetShoot.Normalize();
        float angle = (targetShoot.y > 0) ? Mathf.Rad2Deg * Mathf.Acos(targetShoot.x) : -Mathf.Rad2Deg * Mathf.Acos(targetShoot.x);

        GameObject newBullet = UnityEngine.Object.Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        newBullet.GetComponent<Bullet>().Initialize(gameObject.tag, bulletSpeed, bulletLiveTime);
    }

    private void ShootReload()
    {
        if (bullets > 0)
        {
            Fire();
            bullets--;
            shootTimer.Play();
        }
    }

    public override void Hit()
    {
        if(--hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
