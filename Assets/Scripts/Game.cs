using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Text _startTxt;
    [SerializeField] private GameObject _car;
    [SerializeField] private GameObject _speedometer;

    static public bool BG = false;

    private bool _spaceActivated = false;
    private bool _enableTimer = false;
    private float _timer = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _spaceActivated == false)
        {
            _spaceActivated = true;
            _enableTimer = true;
        }

        if (_enableTimer)
        {
            _timer += Time.deltaTime;
            if (_timer <= 3.5f)
            {
                if (Mathf.Round(_timer) == 0)
                    _startTxt.text = "...3";
                else if (Mathf.Round(_timer) == 1)
                    _startTxt.text = "...2";
                else if (Mathf.Round(_timer) == 2)
                    _startTxt.text = "...1";
                else if (Mathf.Round(_timer) == 3)
                {
                    _startTxt.text = "Start!";
                }

            }
            else
            {
                _enableTimer = false;
                _startTxt.text = "";
                GameStart();
            }
        }
    }

    private void GameStart()
    {
        BG = true;
        Instantiate(_car, new Vector3(0, -2, 0), Quaternion.identity);
        _speedometer.SetActive(true);
    }

}
