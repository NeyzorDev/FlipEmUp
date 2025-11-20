using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
class KamikazeRePosition : State<KamikazeEnemy>
{
    [SerializeField]
    private float rotation = 45.0f;
    [SerializeField]
    private float velocity = 15.0f;



    public override void OnEnter()
    {
        Assert.IsNotNull(owner);


    }

    public override void Update()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        owner.transform.right = p.transform.position - owner.transform.position;
    }

    public bool IsLockedOnTarget()
    {
        return false;
    }
}
