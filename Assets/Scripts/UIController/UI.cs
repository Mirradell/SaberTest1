using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public GameObject camera;

    public Text text;
    public Button button;
    public GameObject startEndPanel;
    public GameObject GameInterface;

    private float timeToStart = -1;
    private void Start()
    {
        SetParameter(false);
    }

    public void OnStartGamePressed()
    {
        timeToStart = Time.time + 4f;
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (timeToStart >= 0)
        {
            text.text = Mathf.FloorToInt(timeToStart - Time.time).ToString();
            if (timeToStart - Time.time <= 0.3f)
            {
                SetParameter(true);
                timeToStart = -1f;
            }
        }
    }

    private void SetParameter(bool changeToOn = true)
    {
        player.GetComponent<PlayerComtroller>().enabled = changeToOn;
        player.GetComponent<Shot>().enabled = changeToOn;
        player.GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = changeToOn;
        boss.GetComponent<BossShot>().enabled = changeToOn;
        camera.GetComponent<ZoomScript>().enabled = changeToOn;
        button.enabled = !changeToOn;
        startEndPanel.SetActive(!changeToOn);
        GameInterface.SetActive(changeToOn);
    }

    public void GameOver()
    {
        GameEnd("Вы проиграли....");
    }

    public void Winning()
    {
        GameEnd("Вы выиграли! Переход ко второй фазе...");
    }

    private void GameEnd(string result)
    {
        SetParameter(false);
        text.text = result;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
