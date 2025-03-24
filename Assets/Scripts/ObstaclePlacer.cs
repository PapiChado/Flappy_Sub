using UnityEngine;
public class ObstaclePlacer : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField][Range(0, 5)] private float randomVerticalDisplacement = 1;
    [SerializeField][Range(0, 30)] private float distance = 5;
    [SerializeField] private Vector3 displacement = Vector3.zero;
    [SerializeField] private Vector3 movementDirection = Vector3.right;
    private Vector3 _currentPosition = Vector3.zero;

    [SerializeField][Range(0.5f, 10f)] private float repeatingRatio = 1;
    [SerializeField][Range(0.5f, 10f)] private float startDelay = 1;

    [SerializeField][Range(0.5f, 30f)] private float destroyDelay = 10;

    Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
        _currentPosition = _startPosition;
        InvokeRepeating(nameof(PlaceObstacles), startDelay, repeatingRatio);
    }

    void PlaceObstacles()
    {
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject obstacle = Instantiate(prefab,
        _currentPosition + distance * movementDirection + displacement + new Vector3(0, Random.Range(0, randomVerticalDisplacement), 0), prefab.transform.rotation, transform);

        _currentPosition = new Vector3(obstacle.transform.position.x, _startPosition.y, obstacle.transform.position.z);

        Destroy(obstacle, destroyDelay);
    }
}