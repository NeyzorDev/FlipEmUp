using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class KamikazeEnemy : Ship
{
    [Header("Behaviors")]
    [SerializeReference]
    private KamikazeRePosition stateReposition = new KamikazeRePosition();
    [SerializeReference]
    private KamikazeTargetLock stateTargetLock = new KamikazeTargetLock();
    [SerializeReference]
    private KamikazeAttack stateAttack = new KamikazeAttack();

    private float speed;

    

    private StateMachine sm;

    public void Start()
    {
        InitializeSM();
    }

    private void InitializeSM()
    {
        sm = new StateMachine();

        stateReposition.SetOwner(this);
        stateTargetLock.SetOwner(this);
        stateAttack.SetOwner(this);

        sm.AddState(stateReposition);
        sm.AddState(stateTargetLock);
        sm.AddState(stateAttack);
        
        sm.AddTransition(stateReposition, stateTargetLock, new Predicate(stateReposition.IsLockedOnTarget));
        sm.AddTransition(stateTargetLock, stateReposition, new Predicate(stateTargetLock.isAttackTime));
        sm.AddTransition(stateAttack, stateReposition, new Predicate(stateAttack.HasPassedAttackLimit));

        sm.SetInitialState(stateReposition);
    }

    // Update is called once per frame
    public void Update()
    {
        sm.Update();
    }

    public override void Hit()
    {
        if (--hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hit();
            Hit();
        }
    }
}

