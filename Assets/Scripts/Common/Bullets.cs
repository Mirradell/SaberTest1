using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float damage = 10f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        var otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.DamageMe(damage);
        }

        Destroy(gameObject);
    }
}
