using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
class KamikazeTargetLock
{
    [SerializeField]
    private float time = 2.2f;

    private Timer attackTimer;



    public void OnEnter()
    {
        attackTimer = new Timer(time);
        attackTimer.Play();

        GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "State Kamikaze Target Lock";
    }

    public void Update()
    {
        attackTimer.Update();
    }


    public bool isAttackTime()
    {
        return !attackTimer.IsPlaying();
    }
}

