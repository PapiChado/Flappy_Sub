using UnityEngine;

public class DestroyOnBulletTrigger : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject spawnOnDestroy;
    [SerializeField][Range(1, 10)] private int maxInstances = 5;
    [SerializeField] private Vector3 randomDelta = Vector3.zero;
    private void OnTriggerEnter(Collider other)
    {
        if (spawnOnDestroy)
        {
            int realInstances = Random.Range(1, maxInstances);

            for (int i = 0; i < realInstances; i++)
            {
                GameObject go = Instantiate(spawnOnDestroy, transform.position +
                randomDelta * Random.Range(-1f, 1f), spawnOnDestroy.transform.rotation);
                go.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
            }
        }

        Destroy(target);
    }
}