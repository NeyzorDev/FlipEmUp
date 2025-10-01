using System;
using UnityEngine;


[Serializable]
public class StandarEnemyShoot : State
{
    private readonly Enemy owner;

    [Header("Fire")]
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

    private readonly Timer shootTimer;
    private int bullets;

    Vector3 targetNormal;

    public StandarEnemyShoot(Enemy owner)
    {
        this.owner = owner;
        shootTimer = new Timer(shootTime, ShootReload);
    }

    public override void OnEnter()
    {
        bullets = bulletsPerCicle;
        shootTimer.Play();
    }

    public override void Update()
    {
        shootTimer.Update();
    }

    public bool IsFinishedShooting()
    {
        return bullets <= 0;
    }

    void Fire()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetShoot = (p) ? p.transform.position - owner.transform.position : new Vector3(1, 0, 0);
        targetShoot.Normalize();
        float angle = (targetShoot.y > 0) ? Mathf.Rad2Deg * Mathf.Acos(targetShoot.x) : -Mathf.Rad2Deg * Mathf.Acos(targetShoot.x);

        GameObject newBullet = UnityEngine.Object.Instantiate(bulletPrefab, owner.transform.position, Quaternion.Euler(0, 0, angle));
        newBullet.GetComponent<Bullet>().Initialize(owner.gameObject.tag, bulletSpeed, bulletLiveTime);
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
}

