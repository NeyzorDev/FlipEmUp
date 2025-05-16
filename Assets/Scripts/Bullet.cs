using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string ownerTag;

    private Vector3 velocity;
    private float liveTime;
    private float liveTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        liveTimer = 0.0f;
    }

    public void Initialize(string oTag, float speed, float liveTime)
    {
        ownerTag = oTag;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != ownerTag)
        {
            Ship s = collision.gameObject.GetComponent<Ship>();
            if (s)
            {
                s.Hit();
            }

            Destroy(gameObject);
        }
    }
}
