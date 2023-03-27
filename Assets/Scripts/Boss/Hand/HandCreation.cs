using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCreation : MonoBehaviour
{
    public Vector3 positionShift = new Vector3(1.05f, 0f, 0f);
    public Material handMaterial;
    public int cubesAmount = 3;
    public GameObject handPart;
    public Transform player;
    public float scale;
    public GameObject deathSurface;
    public bool goingUp = false;
    public float timeToDeath = 3f;
    public int spotsCoef = 3;

    private List<WeakSpot> weakSpots;
    private List<int> weakSpotsNumbers;
    private float time = -1f;
    private GameObject bigLastCube = null;
    private bool doingBoom = true;
    private Vector3 cubeScale;
    private float shiftHeight;
    private GameObject shiftedCube;

    public void CreateHand(Vector3 posDiff)
    {
        InitEverything(posDiff);
        WeakSpots();

        var firstCube = gameObject;
        for (int i = 0; i < cubesAmount; i++)
        {
            firstCube = CreatePart(firstCube.transform, positionShift, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
            if (weakSpotsNumbers.Contains(i))
            {
                weakSpots.Add(firstCube.AddComponent<WeakSpot>());
            }
        }

        bigLastCube = CreatePart(firstCube.transform, positionShift, Quaternion.Euler(0f, 0f, 0f), Vector3.one * 2f);
        bigLastCube.GetComponent<BoxCollider>().isTrigger = true;
        bigLastCube.AddComponent<DeathSurfaceCreation>();
        bigLastCube.GetComponent<DeathSurfaceCreation>().deathSurface = deathSurface;
        bigLastCube.GetComponent<DeathSurfaceCreation>().handHead = gameObject;
    }

    private void InitEverything(Vector3 posDiff)
    {
        scale = 50f;
        positionShift = Vector3.right;
        cubeScale = Vector3.one * scale;
        cubesAmount = Mathf.FloorToInt(Mathf.Sqrt(posDiff.x * posDiff.x + posDiff.z * posDiff.z) / (scale)) - 2;
        //transform.localRotation = Quaternion.Euler(0f, (float)Math.Atan2(posDiff.x, posDiff.z) * 10, 0f);
        //transform.localPosition = new Vector3(0.5f, 0.5f, -0.5f);
       // transform.LookAt(posDiff);
        //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = cubeScale * 0.01f;

        shiftHeight = transform.position.y / (cubesAmount + 3);
        shiftedCube = gameObject;
    }

    private GameObject CreatePart(Transform parent, Vector3 pos, Quaternion rot, Vector3 localScale)
    {
        var cube = Instantiate(handPart, parent);
        cube.transform.localPosition = pos;
        cube.transform.localRotation = rot;
        //cube.transform.LookAt(player);
        cube.transform.localScale = localScale;
        cube.GetComponent<MeshRenderer>().material = handMaterial;
        return cube;
    }

    public void HandBoom()
    {
        shiftedCube.transform.position -= new Vector3(0f, shiftHeight, 0f);
        if (shiftedCube.transform.childCount > 0)
            shiftedCube = shiftedCube.transform.GetChild(0).gameObject;
        else
        {
            doingBoom = false;
            bigLastCube.GetComponent<DeathSurfaceCreation>().Creation();
            shiftedCube = bigLastCube;
        }
    }

    public void HandUp()
    {
        shiftedCube.transform.position += new Vector3(0f, shiftHeight, 0f);
        if (shiftedCube.GetComponent<HandCreation>() == null)
            shiftedCube = shiftedCube.transform.parent.gameObject;
        else
        {
            time = Time.time;
            goingUp = false;
            weakSpots.ForEach(spot => spot.TurnOn(gameObject));
        }
    }

    private void FixedUpdate()
    {
        if (bigLastCube != null)
            if (doingBoom)
                HandBoom();
            else if (goingUp)
                HandUp();
            else if (time > 0 && time + timeToDeath <= Time.time)
            {
                transform.parent.GetComponent<BossShot>().HandDeath();
                Destroy(transform.GetChild(0).gameObject);
                NullEverything();
            }
    }
    private void WeakSpots()
    {
        weakSpotsNumbers = new List<int>();
        weakSpots = new List<WeakSpot>();

        int step = cubesAmount / spotsCoef;
        for (int i = 0; i < spotsCoef; i++)
            weakSpotsNumbers.Add(UnityEngine.Random.Range(i * step, (i + 1) * step + 1));
    }

    private void NullEverything()
    {
        weakSpots = new List<WeakSpot>();
        weakSpotsNumbers = new List<int>();
        time = -1f;
        bigLastCube = null;
        doingBoom = true;
        shiftedCube = null;
        goingUp = false;
    }
}
