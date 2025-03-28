using UnityEngine;
using UnityEngine.Serialization;

public class SubmarineManager : MonoBehaviour
{
    [SerializeField] float fuel = 100f;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float fuelUsageSpeed = 1f;
    [SerializeField] float mineFuelReduction = 5f;

    [SerializeField] Vector3 impulseForce = Vector3.up * 10;
    [SerializeField] Vector3 constantForce = Vector3.up * 20;
    [SerializeField] Vector3 forwardForce = Vector3.right * 20;

    [SerializeField] ForceMode forceMode = ForceMode.Force;

    bool _thrust;
    Rigidbody rb;

    [SerializeField]
    float minRotation = 35;

    [SerializeField]
    float maxRotaion = -35;

    [SerializeField]
    float pitchSpeed = 1;

    [SerializeField]
    float speed = 1;
    
    [SerializeField]
    float maxSpeed = 1;
    
    [FormerlySerializedAs("Impact")] [SerializeField]
    Vector3 impact = new Vector3(0,  0.0f, 0);
    
    [SerializeField]
    Vector3 maxImpact = new Vector3(0,  0.4f, 0);

    [SerializeField]
    Transform ship;

    private bool resetted = false;
    private bool isOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        fuel -= Time.deltaTime * fuelUsageSpeed;
        if (fuel <= 0)
        {
            enabled = false;
            isOver = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            _thrust = true;
            resetted = false;
        }
        else if (Input.GetButton("Jump"))
        {
            Vector3 dest = new Vector3(maxRotaion, ship.transform.localRotation.eulerAngles.y,
            ship.transform.localRotation.eulerAngles.z);
            ship.transform.localRotation = Quaternion.Lerp(ship.transform.localRotation,
            Quaternion.Euler(dest), Time.deltaTime * pitchSpeed);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            _thrust = false;
        }
        else
        {
            Vector3 dest = new Vector3(minRotation, ship.transform.localRotation.eulerAngles.y,ship.transform.localRotation.eulerAngles.z); 

            ship.transform.localRotation = Quaternion.Lerp(ship.transform.localRotation, Quaternion.Euler(dest), Time.deltaTime * pitchSpeed);
        }
    }
    private void FixedUpdate()
    {
        if (impact.y > 0)
        {
            impact.y += 0.01f * Time.deltaTime;
        }
        if (_thrust)
        {
            if (forceMode == ForceMode.Impulse)
            {
                _thrust = false;
                resetted = true;
                rb.AddForce(impulseForce - impact, forceMode);
                ship.transform.localEulerAngles = new Vector3(maxRotaion,
                ship.transform.localRotation.eulerAngles.y, ship.transform.localRotation.eulerAngles.z);
            }
            else
            {
                rb.AddForce(constantForce - impact, forceMode);
            }
        }
        rb.AddForce(forwardForce - impact, forceMode);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger by:{other.gameObject}", other.gameObject);
        if (other.gameObject.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            fuel = Mathf.Clamp(fuel + fuelUsageSpeed, 0, maxFuel);
            Debug.Log($"Fuel gained: {fuel}");
        }
        else if (other.gameObject.CompareTag("Mine"))
        {
            Destroy(other.gameObject);
            fuel = Mathf.Clamp(fuel - mineFuelReduction, 0, maxFuel);
            Debug.Log($"Fuel lost: {fuel}");
            impact += maxImpact;
            Debug.Log($"Speed: {speed}");
            if (fuel <= 0)
            {
                isOver = true;
                RotateOnItself();
                enabled = false;
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        isOver = true;
        rb.isKinematic = true;
        enabled = false;
    }

    private void RotateOnItself()
    {
        if (isOver)
        {
            do
            {
                ship.transform.Rotate(0f, 0f, 1);
            }while (ship.transform.localRotation.eulerAngles.z < 180);
        }
    }
}