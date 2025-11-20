using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
class KamikazeTargetLock : State<KamikazeEnemy>
{
    [SerializeField]
    private float time = 0.2f;

    private Timer attackTimer;



    public override void OnEnter()
    {
        attackTimer = new Timer(time);
        attackTimer.Play();

    }

    public override void Update()
    {
        attackTimer.Update();
    }


    public bool isAttackTime()
    {
        return !attackTimer.IsPlaying();
    }
}

