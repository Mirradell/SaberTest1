using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject hand;

    [SerializeField]
    float bullet_speed = 1f;
    [SerializeField]
    float bullet_damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OneShot();
    }

    private void OneShot()
    {
        var shot = Instantiate(bullet);
        var shot_rb = shot.GetComponent<Rigidbody>();
        shot.transform.position = hand.transform.position + new Vector3(0f, 15f, 0f);
        shot.transform.rotation = hand.transform.rotation;//*/
        shot_rb.AddForce(transform.forward * bullet_speed, ForceMode.Impulse);
        shot.GetComponent<Bullets>().damage = bullet_damage;
    }
}
