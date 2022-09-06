using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private const float MAX_SPEED_ANGLE = -415;
    private const float ZERO_SPEED_ANGLE = -300;

    private Transform arrowTransform;
    public GameObject fuel;
    public Text fuelNumb;
    Image fImg;

    private float speedMax;
    private float speed;

    void Awake()
    {
        arrowTransform = transform.Find("Arrow");
        fImg = fuel.GetComponent<Image>();

        speed = 60f;
        speedMax = 140f;
    }

    void Update()
    {
        ManualMove();

        //speed += 30f * Time.deltaTime;
        //if (speed > speedMax)
        //    speed = speedMax;

        arrowTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());

        fuelNumb.text = CarMove.fuel.ToString();

        if (CarMove.fuel < 80)
        {
            fImg.color = Color.yellow;
            if (CarMove.fuel < 60)
            {
                fImg.color = Color.red;
                if (CarMove.fuel < 40)
                {
                    StartCoroutine(fl_Blink(fImg));
                }
            }
        }
    }

    void ManualMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            float acceleration = 120f;
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = 40f;
            speed -= deceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            float brakeSpeed = 90f;
            speed -= brakeSpeed * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0f, speedMax);
    }
    private float GetSpeedRotation()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speedNormalized = speed / speedMax;
        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

    IEnumerator fl_Blink(Image image)
    {
        Color c = image.color;

        float alpha = 1.0f;

        while (true)
        {
            c.a = Mathf.MoveTowards(c.a, alpha, Time.deltaTime);

            image.color = c;

            if (c.a == alpha)
            {
                if (alpha == 1.0f)
                {
                    alpha = 0.0f;
                }
                else
                    alpha = 1.0f;
            }

            yield return null;
        }
    }
}
