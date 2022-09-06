using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarMove : MonoBehaviour
{
    static public float FUEL = 100;

    [SerializeField] private GameObject _trails;

    private float _speed = 1.5f;
    private Rigidbody2D _rb;
    private readonly float _fuelConsumption = 1f;
    private float _fuelSpeed = 1f;
    private Text _fuelText;
    private bool _gameEnd = false;
    private float _stopTime = 0f;
    private bool _brake = false;
    private bool _trailSpawned = false;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _fuelText = GameObject.FindWithTag("txt").GetComponent<Text>();
    }

    private void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(xAxis * _speed, _rb.velocity.y);
        float yAxis = Input.GetAxis("Vertical");
        _rb.velocity = new Vector2(_rb.velocity.x, yAxis * _speed);
    }

    private void ChangeFuel()
    {
        if (_rb.velocity.y > 0 || _rb.velocity.y < 0 || _rb.velocity.x < 0 || _rb.velocity.x > 0)
        {
            if (Time.time > _fuelSpeed)
            {
                _fuelSpeed = Time.time + _fuelConsumption;
                FUEL -= 3;
                if (FUEL <= 0)
                {
                    FUEL = 0;
                    Time.timeScale = 0;
                    _fuelText.text = "Fuel is empty! Space to restart";
                    _gameEnd = true;
                }
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        _gameEnd = false;
        Time.timeScale = 1;
        FUEL = 100;
    }

    private void Update()
    {
        Move();
        ChangeFuel();

        if (_gameEnd == true && Input.GetButtonDown("Jump"))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _brake = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _brake = false;
            _speed = 1.5f;
            _stopTime = 0;
        }

        if (_brake)
        {
            _stopTime += Time.deltaTime;
            if (_stopTime >= 2 && _trailSpawned == false)
            {
                _speed = 0.5f;
                TrailsSpawn();
                Invoke(nameof(TrailsDestroy), 4f);
            }
        }
    }

    void TrailsSpawn()
    {
        Instantiate(_trails, this.transform.position, Quaternion.identity, transform);
        _trailSpawned = true;
    }

    void TrailsDestroy()
    {
        Destroy(GameObject.FindGameObjectWithTag("trail"));
        _trailSpawned = false;
    }

}
