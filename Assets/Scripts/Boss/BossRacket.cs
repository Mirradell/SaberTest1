using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRacket : MonoBehaviour
{
    [SerializeField]
    GameObject rocket;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float damage;

    private Vector3 correction = new Vector3(0f, 70f, 0f);

    public void CreateRocket()
    {
        var shot = Instantiate(rocket);
        var shot_rb = shot.GetComponent<Rigidbody>();
        shot.transform.position = transform.position + correction;
        //shot.transform.position += transform.rotation.x > 0 ? new Vector3(80f, 0f, 0f) : new Vector3(-80f, 0f, 0f);
        shot.transform.rotation = transform.rotation;
        shot.transform.localScale = transform.localScale / 2;
        shot.transform.LookAt(player.transform);
        shot_rb.AddForce((player.transform.position - transform.position - correction), ForceMode.Impulse);
        shot.GetComponent<Bullets>().damage = damage;
    }

    private void Start()
    {
        
    }
}