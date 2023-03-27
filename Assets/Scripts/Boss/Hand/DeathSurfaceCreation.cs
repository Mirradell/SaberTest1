using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSurfaceCreation : MonoBehaviour
{
    public GameObject deathSurface;
    public GameObject handHead;

    public void Creation()
    {
        var surface = Instantiate(deathSurface, gameObject.transform);
        surface.transform.position = new Vector3(0f, -.1f, 0f);
        surface.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        surface.transform.localScale = new Vector3(100f, 0.1f, 100f);
        var ds = surface.AddComponent<DeathSurface>();
        ds.InitTime();
        ds.HandHead = handHead;
    }
}
