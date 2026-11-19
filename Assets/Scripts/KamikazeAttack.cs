using System;
using UnityEngine;
using UnityEngine.Assertions;


[Serializable]
class KamikazeAttack
{
    [SerializeField]
    private float aceleration = 50.0f;
    [SerializeField]
    private float rotation = 15.0f;

    private Rigidbody2D phy;
    //private float speed;



    public void OnEnter()
    {
        /*Assert.IsNotNull(owner);

        phy = owner.gameObject.GetComponent<Rigidbody2D>();
        */
    }

    public void Update()
    {
        phy.AddForce(new Vector2(aceleration, 0.0f), ForceMode2D.Force);
        HasPassedAttackLimit();
    }

    public bool HasPassedAttackLimit()
    {
        /*GameObject p = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetAttack = (p) ? p.transform.position - owner.transform.position : owner.transform.position - owner.transform.right;

        if(Vector3.Dot(targetAttack, owner.transform.right) < 0)
        {
            return true;
        }*/

        return false;
    }
}

