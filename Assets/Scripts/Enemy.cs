using UnityEngine;

public class Enemy : Ship
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 15.0f;
    [SerializeField]
    private Vector3 offsetPosition = new Vector3(4.0f, 0.0f, 0.0f);
    [SerializeField]
    private float angleOffset = 30.0f;

    [Header("Fire")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shootTime = 0.5f;
    [SerializeField]
    private float bulletSpeed = 12.0f;
    [SerializeField]
    private float bulletLiveTime = 4.0f;
    [SerializeField]
    private int bulletsPerCicle = 3;

    Vector3 originalPos;
    Vector3 targetPos;
    Timer shootTimer;

    private Vector3 velocity;
    private bool isMoving;

    // OnMove Vars
    Vector3 targetNormal;

    // OnShoot Vars
    int bullets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootTimer = new Timer(shootTime, ShootReload);

        isMoving = true;
        OnEnterMove();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (isMoving)
        {
            UpdateMove(dt);
        }
        else
        {
            UpdateShoot(dt);
        }
    }

    void Fire()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetShoot = (p) ? p.transform.position - transform.position : new Vector3(1, 0, 0);
        targetShoot.Normalize();
        float angle = (targetShoot.y > 0) ? Mathf.Rad2Deg * Mathf.Acos(targetShoot.x) : -Mathf.Rad2Deg * Mathf.Acos(targetShoot.x);
        
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        newBullet.GetComponent<Bullet>().Initialize(gameObject.tag, bulletSpeed, bulletLiveTime);
    }

    private void ShootReload()
    {
        if(bullets > 0)
        {
            Fire();
            bullets--;
            shootTimer.Play();
        }
        else
        {
            isMoving = true;
            OnEnterMove();
        }
    }

    public override void Hit()
    {
        if(--hitPoints<= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnterMove()
    {
        // Select where and how is going to move.
        originalPos = transform.position;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        targetPos = (p) ? p.transform.position + offsetPosition : originalPos;

        velocity = targetPos - originalPos;
        velocity.Normalize();
        velocity *= speed;

        targetNormal = targetPos - originalPos;
        targetNormal.Normalize();
    }

    private void UpdateMove(float dt)
    {
        Vector3 deltaVel = velocity * dt;
        transform.position += deltaVel;

        Vector3 targetDistance = targetPos - transform.position;
        targetDistance.Normalize();
        if (Vector3.Dot(targetDistance, targetNormal) < 0.0f)
        {
            isMoving = false;
            OnExitMove();
            OnEnterShoot();
        }
    }

    private void OnExitMove()
    {
        Quaternion offsetRotation = Quaternion.AngleAxis(angleOffset, new Vector3(0.0f, 0.0f, 1.0f));
        offsetPosition = offsetRotation * offsetPosition;
    }

    private void OnEnterShoot()
    {
        bullets = bulletsPerCicle;
        shootTimer.Play();
    }

    private void UpdateShoot(float dt)
    {
        shootTimer.Update();
    }

    private void OnExitShoot()
    {

    }

}
