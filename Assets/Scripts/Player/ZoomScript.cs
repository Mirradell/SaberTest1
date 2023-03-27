using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    public Texture aim;

    [SerializeField]
    private float zoomSpeed = 1f;
    [SerializeField]
    float aimSize = 32f;
    [SerializeField]
    float aimSpeed = 2f;

    float zoomMax = 60f;
    float zoomMin = 10f;
    float coef = 1f;
    float camMax = 256f;
    [SerializeField]
    float curAim;
    new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
        curAim = aimSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            coef = -1f;
        if (Input.GetMouseButtonUp(1))
            coef = 1f;
            Zoom();
    }

    private void Zoom()
    {
        var camera = gameObject.GetComponent<Camera>();
        var newView = camera.fieldOfView + coef * zoomSpeed;
        if (newView >= zoomMin && newView <= zoomMax)
            camera.fieldOfView = newView;
    }

    private void OnGUI()
    {
        var tmpAim = curAim - coef * aimSpeed;

        if (tmpAim >= aimSize && tmpAim <= camMax)
            curAim = tmpAim;
        float posX = camera.pixelWidth / 2 - curAim / 4;
        float posY = camera.pixelHeight / 2 - curAim / 4;
        GUI.Label(new Rect(posX, posY, curAim, curAim), aim);
    }
}
