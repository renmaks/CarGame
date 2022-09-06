using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public bool spaceActivated = false;
    public Text startTxt;
    public GameObject car;
    bool enableTimer = false;
    public GameObject speedometer;
    public float timer = 0;
    static public bool bG = false;

    void Awake()
    {
  
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && spaceActivated == false)
        {
            spaceActivated = true;
            enableTimer = true;
        }

        if (enableTimer)
        {
            timer += Time.deltaTime;
            if (timer <= 3.5f)
            {
                if (Mathf.Round(timer) == 0)
                    startTxt.text = "...3";
                else if (Mathf.Round(timer) == 1)
                    startTxt.text = "...2";
                else if (Mathf.Round(timer) == 2)
                    startTxt.text = "...1";
                else if (Mathf.Round(timer) == 3)
                {
                    startTxt.text = "Start!";
                }

            }
            else
            {
                enableTimer = false;
                startTxt.text = "";
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        bG = true;
        Instantiate(car, new Vector3(0, -2, 0), Quaternion.identity);
        speedometer.SetActive(true);
    }

}
