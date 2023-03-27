using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthMax = 100f;
    public float healthLeft;
    public float warningLvl = 0.3f;
    public GameObject objectToWarning;
    public UI ui;

    public RectTransform myHealth;
    public Text oppScores;

    bool isWarning = false;
    Color mainColor;
    Material material;

    bool toColor = false;
    private int oppScore = 0;
    private float coef;

    // Start is called before the first frame update
    void Start()
    {
        healthLeft = healthMax;
        coef = healthMax / 100;
        if (objectToWarning != null)
        {
            material = objectToWarning.GetComponent<MeshRenderer>().material;
        }
        else
        {
            material = GetComponent<MeshRenderer>().material;
        }
        mainColor = material.color;
        myHealth.sizeDelta = new Vector2(healthLeft * coef, myHealth.sizeDelta.y);
        oppScores.text = "000000";
    }

    // Update is called once per frame
    void Update()
    {
        if (isWarning) Warning();
    }

    public void DamageMe(float damage)
    {
        healthLeft -= damage;
        myHealth.sizeDelta = new Vector2(healthLeft * coef, myHealth.sizeDelta.y);
        oppScore += Mathf.FloorToInt(damage * 10);
        oppScores.text = System.String.Format("{0:d6}", oppScore);
        if (healthLeft <= 0f)
                   Death();
        //Destroy(gameObject);
        if (healthLeft <= healthMax * warningLvl && !isWarning)
            isWarning = true;
    }

    public void Death()
    {
        if (tag == "Boss")
            ui.Winning();
        else if (tag == "Player")
            ui.GameOver();
        else
            Destroy(gameObject);
    }

    public void Warning()
    {
        float coef = toColor ? 0.01f : -0.01f;
        material.color = material.color - coef * (Color.white - mainColor);
        if (material.color == Color.white || material.color == mainColor)
            toColor = !toColor;
    }
}
