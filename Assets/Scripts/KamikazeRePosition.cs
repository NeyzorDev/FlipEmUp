using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
class KamikazeRePosition
{
    [SerializeField]
    private float distance = 10.0f;
    [SerializeField]
    private float rotation = 0.0f;
    [SerializeField]
    private float velocity = 12.8f;

    private Rigidbody2D phy;


    public void OnEnter()
    {
        /*Assert.IsNotNull(owner);

        phy = owner.GetComponent<Rigidbody2D>();
        */
        GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "State Kamikaze Re Position";
    }

    public void Update()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        /*owner.transform.right = p.transform.position - owner.transform.position;

        phy.linearVelocity = velocity * new Vector2(owner.transform.right.x, owner.transform.right.y);
        */
    }

    public bool IsLockedOnTarget()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        /*if((p.transform.position - owner.transform.position).sqrMagnitude < distance* distance)
        {
            //phy.linearVelocity = Vector2.zero;
            // Decelerate till 0 velocity.
            return true;
        }*/
        return false;
    }
}
