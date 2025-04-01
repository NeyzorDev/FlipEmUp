using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    protected int hitPoints = 1;


    public abstract void Hit();
}