using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarMove : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;

    static public float fuel = 100;

    float fuelConsumption = 1f;
    float fuelSpeed = 1f;
    Text t;
    bool gameEnd = false;
    float stopTime = 0f;
    public GameObject trails;
    public bool brake = false;
    public bool trailSpawned = false;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GameObject.FindWithTag("txt").GetComponent<Text>();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
        float yAxis = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, yAxis * speed);

        if (rb.velocity.y > 0 || rb.velocity.y < 0 || rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Time.time > fuelSpeed)
            {
                fuelSpeed = Time.time + fuelConsumption;
                fuel -= 3;
                if (fuel <= 0)
                {
                    fuel = 0;
                    Time.timeScale = 0;
                    t.text = "Fuel is empty! Space to restart";
                    gameEnd = true;
                }
            }
        }

        if (gameEnd == true && Input.GetButtonDown("Jump"))
        {
           SceneManager.LoadScene(0);
            gameEnd = false;
            Time.timeScale = 1;
            fuel = 100;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            brake = true;
            
        //    if (stopTime >= 2 && stopTime < 3)
        //    {
        //        speed = 1f;
        //        TrailsSpawn();
        //        Invoke("TrailsDestroy", 4f);
        //    }
        //}
        //else
        //{
        //    speed = 2.5f;
        //    stopTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            brake = false;
            speed = 1.5f;
            stopTime = 0;
        }

        if (brake)
        {
            stopTime += Time.deltaTime;
            if (stopTime >= 2 && trailSpawned == false)
            {
                speed = 0.5f;
                TrailsSpawn();
                Invoke("TrailsDestroy", 4f);
            }
        }
    }

    void TrailsSpawn()
    {
        Instantiate(trails, this.transform.position, Quaternion.identity, transform);
        trailSpawned = true;
    }

    void TrailsDestroy()
    {
        Destroy(GameObject.FindGameObjectWithTag("trail"));
        trailSpawned = false;
    }

}
