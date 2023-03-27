using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float stepBetweenRockets;
    public float stepBetweenHands;
    public float minStep = 1f;
    
    public HandCreation hand;
    public GameObject player;

    private float nextRocketTime;
    private float nextHandTime;
    private bool isHandOn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (minStep > stepBetweenHands && minStep > stepBetweenRockets)
            throw new System.Exception("Шаги некорректны!");

        nextHandTime = Random.Range(minStep, stepBetweenHands) + Time.time;
        nextRocketTime = Random.Range(minStep, stepBetweenRockets) + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextRocketTime)
        {
            nextRocketTime = Random.Range(minStep, stepBetweenRockets) + Time.time;
            GetComponent<BossRacket>().CreateRocket();
        }

        if (Time.time >= nextHandTime && !isHandOn)
        {
            isHandOn = true;
            transform.LookAt(player.transform);
            hand.CreateHand(transform.position - player.transform.position);
        }

    }

    public void HandDeath()
    {
        isHandOn = false;
        nextHandTime = Random.Range(minStep, stepBetweenHands) + Time.time;
    }
}
