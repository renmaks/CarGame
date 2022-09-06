using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Для редактирования")]
    [SerializeField] private float _speed;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (Game.BG)
        {
            float offset = Mathf.Repeat(Time.time * _speed, 6);
            transform.position = _startPosition + new Vector3(0, -offset, 0);
        }
    }
}
