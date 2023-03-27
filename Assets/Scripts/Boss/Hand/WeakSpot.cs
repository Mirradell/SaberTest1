using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject bossParent;
    private Health bossHealth;

    private bool toColor = false;
    private Material material;
    private Color mainEmission = new Color(0.8f, .8f, .8f);

    // Update is called once per frame
    void Update()
    {
        if (bossParent != null && bossHealth != null)
        {
            float coef = toColor ? 0.01f : -0.01f;
            material.color = material.color - coef * (Color.white - mainEmission);
            if (material.color == Color.white || material.color == mainEmission)
                toColor = !toColor;
        }

        
    }

    public void TurnOn(GameObject boss)
    {
        bossParent = boss;
        bossHealth = boss.GetComponent<Health>();
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
            bossHealth.DamageMe(other.GetComponent<Bullets>().damage);
    }
}
