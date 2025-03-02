using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velocity;
    private float liveTime;
    private float liveTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        liveTimer = 0.0f;
    }

    public void Initialize(float speed, float liveTime)
    {
        float z = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        velocity = new Vector3(Mathf.Cos(z), Mathf.Sin(z), 0.0f) * speed;
        this.liveTime = liveTime;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        liveTimer += dt;
        if(liveTimer > liveTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;

        transform.position += velocity * dt;
    }
}
