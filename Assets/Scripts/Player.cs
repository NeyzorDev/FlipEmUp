using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float maxVelocity = 3.0f;
    [SerializeField]
    private float minVelocity = 0.01f;
    [SerializeField]
    private float velocity = 2.0f;
    //[SerializeField]
    //private float deceleration = 0.97f;

    [Header("Debug")]
    [SerializeField]
    private Vector2 currVelocity;

    [SerializeField]
    private float angle;
    private readonly float HalfSqrt2 = Mathf.Sqrt(2.0f) / 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        float moveX = Input.GetAxisRaw("MoveX");
        float moveY = Input.GetAxisRaw("MoveY");

        //Movement update.
        if(moveX != 0.0f || moveY != 0.0f)
        {
            //Movement update.
            Vector2 dVel = new Vector2(moveX * velocity * dt, moveY * velocity * dt);
            if (moveX != 0.0f || moveY != 0.0f) { dVel *= HalfSqrt2; }

            currVelocity += dVel;

            // Max speed cap.
            if(currVelocity.sqrMagnitude > maxVelocity * maxVelocity)
            {
                // Can be improved. Shouldnt be done with sqr.
                currVelocity = currVelocity / currVelocity.sqrMagnitude * maxVelocity * maxVelocity;
            }
        }
        else
        {
            // No movement input.
            //currVelocity *= deceleration;
            //if(currVelocity.sqrMagnitude < minVelocity)
            {
                currVelocity = Vector2.zero;
            }
        }

        // Rotation update.
        Vector2 playerForward = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        playerForward.Normalize();

        float angleCos = Mathf.Rad2Deg * Mathf.Acos(playerForward.x);
        float angleSin = Mathf.Asin(playerForward.y);
        angle = (angleSin > 0.0f) ? angleCos : -angleCos;

    }

    private void FixedUpdate()
    {
        // Update player transform.
        transform.position += new Vector3(currVelocity.x, currVelocity.y, transform.position.z);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
}
