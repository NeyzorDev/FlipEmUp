using UnityEngine;

public class Enemy : Ship
{
    [Header("Movement")]
    [SerializeField]
    private float velocity = 1.0f;

    [Header("Fire")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shootTime = 2.0f;
    [SerializeField]
    private float bulletSpeed = 12.0f;
    [SerializeField]
    private float bulletLiveTime = 4.0f;

    Timer shootTimer;

    private Vector2 target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootTimer = new Timer(shootTime, ShootReload);
        shootTimer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer.Update();
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.GetComponent<Bullet>().Initialize(gameObject.tag, bulletSpeed, bulletLiveTime);
    }

    private void ShootReload()
    {
        Fire();
        shootTimer.Play();
    }

    public override void Hit()
    {
        if(--hitPoints<= 0)
        {
            Destroy(gameObject);
        }
    }
}
