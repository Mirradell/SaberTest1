using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathSurface : MonoBehaviour
{
    public float lifeTime = 2f;
    public GameObject HandHead;
    private float creationTime;
    public float damage = 10f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= creationTime + lifeTime)
            DestroyMe();
    }

    public void InitTime()
    {
        creationTime = Time.time;
    }

    public void DestroyMe()
    {
        HandHead.GetComponent<HandCreation>().goingUp = true;

        Destroy(gameObject);
        Debug.Log("Destroy death surface");
    }
}
