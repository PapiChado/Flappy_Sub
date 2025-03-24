using UnityEngine;
public class FloatingAnimation : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float delta = 1;
    private Vector3 _startPosition;
    private void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        transform.position = _startPosition + delta * Mathf.Sin(speed * Time.time) * direction;
    }
}