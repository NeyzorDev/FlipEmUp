using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform playerInst;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInst = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerInst.position.x, playerInst.position.y, transform.position.z);
    }
}
