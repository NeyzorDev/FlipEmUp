using System;
using UnityEngine;
using UnityEngine.Assertions;


[Serializable]
public class StandarEnemyMove : State<Enemy>
{
    [SerializeField]
    private float speed = 15.0f;
    [SerializeField]
    private Vector3 offsetPosition = new Vector3(4.0f, 0.0f, 0.0f);
    [SerializeField]
    private float angleOffset = 30.0f;

    private Vector3 originalPos;
    private Vector3 targetPos;
    private Vector3 currOffsetPos;

    private Vector3 velocity;

    private Vector3 targetNormal;

    public StandarEnemyMove()
    { 
        currOffsetPos = offsetPosition;
    }

    /*public void SetOwner(Enemy owner)
    {
        if(this.owner == null)
        {
            this.owner = owner;
        }
    }*/

    public override void OnEnter()
    {
        Assert.IsNotNull(owner);

        // Select where and how is going to move.
        originalPos = owner.transform.position;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        targetPos = (p) ? p.transform.position + currOffsetPos : originalPos;

        velocity = targetPos - originalPos;
        velocity.Normalize();
        velocity *= speed;

        targetNormal = targetPos - originalPos;
        targetNormal.Normalize();
    }

    public override void Update()
    {
        float dt = Time.deltaTime;

        Vector3 deltaVel = velocity * dt;
        owner.transform.position += deltaVel;
    }

    public override void OnExit()
    {
        // Adjust offset target position.
        Quaternion offsetRotation = Quaternion.AngleAxis(angleOffset, new Vector3(0.0f, 0.0f, 1.0f));
        currOffsetPos = offsetRotation * currOffsetPos;
    }

    public bool IsReadyToShoot()
    {
        Vector3 targetDistance = targetPos - owner.transform.position;
        targetDistance.Normalize();

        return (Vector3.Dot(targetDistance, targetNormal) < 0.0f);
    }

}
