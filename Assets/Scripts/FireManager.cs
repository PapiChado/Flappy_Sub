using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float firePower = 100;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private ForceMode fireMode = ForceMode.Impulse;
    [SerializeField] private Transform[] firePositions;
    [SerializeField] private Transform root;

    float _fireTimer = 0;
    private void Update()
    {

        _fireTimer += Time.deltaTime;
        if (_fireTimer < fireRate) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log($"Fire1: {transform.eulerAngles}");

            for (int i = 0; i < firePositions.Length; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab, firePositions[i].position, firePositions[i].rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(firePositions[i].forward * firePower, fireMode); //or Vector3.down
            }

            _fireTimer = 0;
        }
    }
}