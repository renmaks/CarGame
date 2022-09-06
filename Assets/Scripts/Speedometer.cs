using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private GameObject _fuel;
    [SerializeField] private Text _fuelNumb;

    private const float MAX_SPEED_ANGLE = -415;
    private const float ZERO_SPEED_ANGLE = -300;
    private Transform _arrowTransform;
    private Image _fuelImg;
    private float _speedMax;
    private float _speed;

    void Awake()
    {
        _arrowTransform = transform.Find("Arrow");
        _fuelImg = _fuel.GetComponent<Image>();

        _speed = 60f;
        _speedMax = 140f;
    }

    void Update()
    {
        ManualMove();

        _arrowTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());

        _fuelNumb.text = CarMove.FUEL.ToString();

        if (CarMove.FUEL < 80)
        {
            _fuelImg.color = Color.yellow;
            if (CarMove.FUEL < 60)
            {
                _fuelImg.color = Color.red;
                if (CarMove.FUEL < 40)
                {
                    StartCoroutine(FuelBlink(_fuelImg));
                }
            }
        }
    }

    void ManualMove()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            float acceleration = 120f;
            _speed += acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = 40f;
            _speed -= deceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            float brakeSpeed = 90f;
            _speed -= brakeSpeed * Time.deltaTime;
        }

        _speed = Mathf.Clamp(_speed, 0f, _speedMax);
    }
    private float GetSpeedRotation()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float speedNormalized = _speed / _speedMax;
        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

    IEnumerator FuelBlink(Image image)
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
